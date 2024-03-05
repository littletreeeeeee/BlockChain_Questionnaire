pragma solidity ^0.8.17;

interface IERC20 {
    event Transfer(address indexed from, address indexed to, uint256 value);

    event Approval(address indexed owner, address indexed spender, uint256 value);

    function totalSupply() external view returns (uint256);

    function balanceOf(address account) external view returns (uint256);
    function allowance(address owner, address spender) external view returns (uint256);
    
    function approve(address spender, uint256 amount) external returns (bool);
    function transfer(address to, uint256 amount) external returns (bool);
    function transferFrom(address from, address to, uint256 amount) external returns (bool);
  

}

contract MiaoToken is IERC20 {
    uint256 _totalSupply;
    mapping(address => uint256) _balance;
    mapping(address => mapping(address => uint256)) _allowance;
    string _name;
    string _symbol;
    address _owner;

     // 問卷結構
    struct Survey {
        uint256 reward; // 填寫獎勵
        uint256 openReward; // 公開獎勵
        uint256 publishQty; // 發行數量
        uint256 lotteryCount; // 抽獎獎勵數量
        uint256 lottery; // 抽獎金額
        uint256 deadline; // 填寫期限
        string ipfsAddress; // 問卷題目地址
        bool isClosed; // 是否關閉問卷
        address creator;
    }
    struct Answers {
        bool isWinner; //是否中獎
        string answerIpfsAddress; //答案的IPFS路徑
    }
    // 存放問卷的映射表
    mapping(string => Survey) private surveys;
    mapping(string => address[]) private surveyFillAddress;

    //判斷是否已經存在
    mapping(string => bool) public settingExists;

    mapping(string => mapping(address => bool)) public surveyRespondents;
    mapping(string => mapping(address => Answers)) public surveyFillUsers;
    mapping(string => uint256) public fillUserCount;
    mapping(string => uint256) public openUserCount;


    modifier onlyOwner() {
        require(_owner == msg.sender, "ERROR : You don't have permission");
        _;
    }

     constructor() public payable {
        _owner =msg.sender;
        _symbol = "Miao";
        _name = "Miao Miao Token";
        _totalSupply = 1000000 * 10**uint(decimals());
        _balance[_owner] = _totalSupply;
        emit Transfer(address(0), _owner, _totalSupply);
    }

    function mint(address account, uint256 amount) public onlyOwner {
        require(account != address(0), "ERROR: mint to address 0");
        _totalSupply += amount;
        _balance[account] += amount;
        emit Transfer(address(0), account, amount);
    }

    function burn(address account, uint256 amount) public onlyOwner {
        require(account != address(0), "ERROR: burn from address 0");
        uint256 accountBalance = _balance[account];
        require(accountBalance >= amount, "ERROR: no more token to burn");
        _balance[account] = accountBalance - amount;
        _totalSupply -= amount;
        emit Transfer(account, address(0), amount);
    }

    function name() public view returns (string memory) {
        return _name;
    }
    function symbol() public view returns (string memory) {
        return _symbol;
    }
    function decimals() public pure returns (uint8) {
        return 18;
    }

    function totalSupply() public view returns (uint256) {
        return _totalSupply;
    }

    function balanceOf(address account) public view returns (uint256) {
        return _balance[account];
    }

    function _transfer(address from, address to, uint256 amount) internal {
        uint256 myBalance = _balance[from];
        require(myBalance >= amount, "No money to transfer");
        require(to != address(0), "Transfer to address 0");

        _balance[from] = myBalance - amount;
        _balance[to] = _balance[to] + amount;
        emit Transfer(from, to, amount);
    }

    function transfer(address to, uint256 amount) public returns (bool) {
        _transfer(msg.sender, to, amount);
        return true;
    }

    function allowance(address owner, address spender) public view returns (uint256) {
        return _allowance[owner][spender];
    }

    function _approve(address owner, address spender, uint256 amount) internal {
        _allowance[owner][spender] = amount;
        emit Approval(owner, spender, amount);
    }
    
    function approve(address spender, uint256 amount) public returns (bool) {
        _approve(msg.sender, spender, amount);
        return true;
    }

    function transferFromContract(address recipient, uint256 amount) public returns (bool) {
        address sender = address(this);
        require(_balance[sender] >= amount, "Insufficient balance");
        _balance[sender] -= amount;
        _balance[recipient] += amount;
        emit Transfer(sender, recipient, amount);
        return true;
    }
    function PublishSurvey(address creator,address recipient, uint256 amount) public returns (bool) {
        require(_balance[creator] >= amount, "Insufficient balance");
        _balance[creator] -= amount;
        _balance[recipient] += amount;

        emit Transfer(creator, recipient, amount);
        return true;
    }

    function transferFrom(address from, address to, uint256 amount) public returns (bool) {
        uint256 myAllowance = _allowance[from][msg.sender];
        require(myAllowance >= amount, "ERROR: myAllowance < amount");

        _approve(from, msg.sender, myAllowance - amount);
        _transfer(from, to, amount);
        return true;
    }

    event PurchaseToken(address indexed from, address indexed to, uint256 value, uint256 rate);

     function purchase(uint256 _value,uint256 exchangeRate) external payable {
        require(msg.value >= _value / exchangeRate, "Not enough ETH");
        require(_balance[_owner] >= _value, "Not enough MiaoToken");

            _balance[msg.sender] += _value;
            _balance[_owner] -= _value;
            emit PurchaseToken(_owner, msg.sender, _value,exchangeRate);
        
    }

    event WithdrawETH(address indexed from, address indexed to, uint256 value);
      function withdraw() public onlyOwner {
        //提領$$
        address payable owner = payable(msg.sender);
        owner.transfer(address(this).balance);
        emit WithdrawETH(address(this),owner,address(this).balance);
    }



    
    //LOG


   event CreateSurvey(address creatorAddress,uint256 MiaoTokenForContract,address indexed ContractAddress,uint256 MiaoTokenForOwner,address indexed OwnerAddress);
   event LogTimeStamp(uint256 deadline, uint256 nowTime);

    // 建立問卷
    function createSurvey(string memory docId, uint256 reward,uint256 openReward, uint256 publishQty, uint256 lotteryCount, uint256 lottery, uint256 deadline, string memory ipfsAddress) external {

      emit LogTimeStamp(deadline,block.timestamp);
      require(!settingExists[docId], "Survey setting already exists");

      surveys[docId] = Survey(reward,openReward, publishQty, lotteryCount, lottery, deadline, ipfsAddress, false,msg.sender);
      settingExists[docId] = true;

        //發送10%給owner，其他發給合約 填答獎勵+抽獎獎勵+公開獎勵
      uint256 total = reward * publishQty + lottery * lotteryCount+openReward* publishQty;

      uint256 forOwner = total * 1 / 10;

      require(transfer(_owner, forOwner), "Token to owner transfer failed");
      require(transfer(address(this), total), "Token to contract transfer failed");
      emit  CreateSurvey(msg.sender,total,address(this),forOwner,_owner);

    }

    event FillSurvey(string docId,address indexed RewardFrom,address indexed RewardTo,uint256 Reward,uint256 openReward,uint256 fillCount);

    // 填寫問卷
    function fillSurvey(address from,string memory docId, string memory ipfsAddress,bool isOpen ) external {
        require(!surveys[docId].isClosed, "Survey already closed");
        //require(surveys[docId].deadline > block.timestamp, "Deadline has passed");
        require(surveys[docId].publishQty > fillUserCount[docId], "No survey can fill! ");
        require(!surveyRespondents[docId][from], "You have already responded to the survey");

        // 將填寫者加入是否有填寫的清單
        surveyRespondents[docId][from] = true;
        //將填寫的答案加入fillUser
        surveyFillUsers[docId][from] = Answers(false, ipfsAddress);
        surveyFillAddress[docId].push(from);

        fillUserCount[docId]=fillUserCount[docId]+1;
        // 發送填寫獎勵 (from contract)
        require(transferFromContract(from, surveys[docId].reward), "Token transfer failed");
        //發送公開獎勵 (from contract )
        uint256 oReward=0;
        if(isOpen){
        require(transferFromContract(from, surveys[docId].openReward), "Token transfer failed");
        oReward= surveys[docId].openReward;
        openUserCount[docId]=openUserCount[docId]+1;
        }
       
        emit FillSurvey(docId,address(this),from,surveys[docId].reward,oReward,fillUserCount[docId]);

    }

    event CloseSurvey(string docId,uint256 SurveyFillCount,uint256 returnReward);
    event LotteryLog(string docId,address indexed winnerAddress,uint256 reward);
    event TestLog(string docId,uint256 fillCount,address[] addressList);
    // 關閉問卷
    function closeSurvey(string memory docId) external {
        require(!surveys[docId].isClosed, "Survey already closed");
        require(block.timestamp > surveys[docId].deadline, "Deadline has not passed yet");
        address[] memory respondents= getRespondents(docId);
        emit TestLog(docId,fillUserCount[docId],respondents);
       
     
       uint256 count = 0;
        for (uint256 i = 0; i <  surveys[docId].lotteryCount; i++) {
            uint256 index = uint256(keccak256(abi.encodePacked(block.timestamp, block.difficulty, i))) % respondents.length;
            require(transferFromContract(respondents[index], surveys[docId].lottery), "Token transfer failed");
            emit LotteryLog(docId,respondents[index],surveys[docId].lottery);

            count++;
            if (count >= respondents.length) {
                break;
            }
        }
       uint256 returnOpenReward=0;
       if(openUserCount[docId]!=surveys[docId].publishQty){  //要歸還之前已經扣款要?有公開資料的人的獎勵  (全部發行數量-已公開數量)*公開獎勵
            uint256 notOpenCount=surveys[docId].publishQty-openUserCount[docId];
            returnOpenReward=notOpenCount*surveys[docId].openReward;

            
            require(transferFromContract(surveys[docId].creator, returnOpenReward), "Token transfer failed");

       }
       uint256 returnReward=0;
        if(respondents.length!=surveys[docId].publishQty){ //歸還沒有填答的獎勵
           
            uint256 notFillSurveyCount=surveys[docId].publishQty-respondents.length; 
            returnReward=notFillSurveyCount*surveys[docId].reward;

            require(transferFromContract(surveys[docId].creator, returnReward), "Token transfer failed");

       }
        surveys[docId].isClosed = true;
        emit CloseSurvey(docId,fillUserCount[docId],returnReward);
   
    }

   function getRespondents(string memory docId) public view returns (address[] memory) {
    uint256 count = 0;
    address[] storage fillUsersAddr = surveyFillAddress[docId];
    address[] memory respondents = new address[]( fillUserCount[docId]);

    for (uint256 i = 0; i < fillUserCount[docId]; i++) {
            respondents[count] = fillUsersAddr[i];
            count++;
        
    }

    return respondents;
}

function getSurvey(string memory docId) public view returns (bytes memory) {
    Survey storage survey = surveys[docId];
    return abi.encodePacked(
        '{"reward":', uint2str(survey.reward),
        ',"publishQty":', uint2str(survey.publishQty),
        ',"lotteryCount":', uint2str(survey.lotteryCount),
        ',"lottery":', uint2str(survey.lottery),
        ',"deadline":', uint2str(survey.deadline),
        ',"ipfsAddress":"', survey.ipfsAddress,
        '","isClosed":', survey.isClosed ? "true" : "false",
        '}'
    );
}

function getFillSurvey(string memory docId,address from) public view returns (bytes memory) {
    Answers storage fillUser = surveyFillUsers[docId][from];
    return abi.encodePacked(
        '{"isWinner":', fillUser.isWinner ? "true" : "false",
        ',"ipfsAddress":"', fillUser.answerIpfsAddress,
        '"}'
    );
}
function uint2str(uint256 num) private pure returns (string memory) {
    if (num == 0) {
        return "0";
    }
    uint256 temp = num;
    uint256 digits;
    while (temp != 0) {
        digits++;
        temp /= 10;
    }
    bytes memory buffer = new bytes(digits);
    while (num != 0) {
        digits -= 1;
        buffer[digits] = bytes1(uint8(48 + uint256(num % 10)));
        num /= 10;
    }
    return string(buffer);
}


}