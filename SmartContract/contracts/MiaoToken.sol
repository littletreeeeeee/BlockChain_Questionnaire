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

     // �ݨ����c
    struct Survey {
        uint256 reward; // ��g���y
        uint256 openReward; // ���}���y
        uint256 publishQty; // �o��ƶq
        uint256 lotteryCount; // ������y�ƶq
        uint256 lottery; // ������B
        uint256 deadline; // ��g����
        string ipfsAddress; // �ݨ��D�ئa�}
        bool isClosed; // �O�_�����ݨ�
        address creator;
    }
    struct Answers {
        bool isWinner; //�O�_����
        string answerIpfsAddress; //���ת�IPFS���|
    }
    // �s��ݨ����M�g��
    mapping(string => Survey) private surveys;
    mapping(string => address[]) private surveyFillAddress;

    //�P�_�O�_�w�g�s�b
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
        //����$$
        address payable owner = payable(msg.sender);
        owner.transfer(address(this).balance);
        emit WithdrawETH(address(this),owner,address(this).balance);
    }



    
    //LOG


   event CreateSurvey(address creatorAddress,uint256 MiaoTokenForContract,address indexed ContractAddress,uint256 MiaoTokenForOwner,address indexed OwnerAddress);
   event LogTimeStamp(uint256 deadline, uint256 nowTime);

    // �إ߰ݨ�
    function createSurvey(string memory docId, uint256 reward,uint256 openReward, uint256 publishQty, uint256 lotteryCount, uint256 lottery, uint256 deadline, string memory ipfsAddress) external {

      emit LogTimeStamp(deadline,block.timestamp);
      require(!settingExists[docId], "Survey setting already exists");

      surveys[docId] = Survey(reward,openReward, publishQty, lotteryCount, lottery, deadline, ipfsAddress, false,msg.sender);
      settingExists[docId] = true;

        //�o�e10%��owner�A��L�o���X�� �񵪼��y+������y+���}���y
      uint256 total = reward * publishQty + lottery * lotteryCount+openReward* publishQty;

      uint256 forOwner = total * 1 / 10;

      require(transfer(_owner, forOwner), "Token to owner transfer failed");
      require(transfer(address(this), total), "Token to contract transfer failed");
      emit  CreateSurvey(msg.sender,total,address(this),forOwner,_owner);

    }

    event FillSurvey(string docId,address indexed RewardFrom,address indexed RewardTo,uint256 Reward,uint256 openReward,uint256 fillCount);

    // ��g�ݨ�
    function fillSurvey(address from,string memory docId, string memory ipfsAddress,bool isOpen ) external {
        require(!surveys[docId].isClosed, "Survey already closed");
        //require(surveys[docId].deadline > block.timestamp, "Deadline has passed");
        require(surveys[docId].publishQty > fillUserCount[docId], "No survey can fill! ");
        require(!surveyRespondents[docId][from], "You have already responded to the survey");

        // �N��g�̥[�J�O�_����g���M��
        surveyRespondents[docId][from] = true;
        //�N��g�����ץ[�JfillUser
        surveyFillUsers[docId][from] = Answers(false, ipfsAddress);
        surveyFillAddress[docId].push(from);

        fillUserCount[docId]=fillUserCount[docId]+1;
        // �o�e��g���y (from contract)
        require(transferFromContract(from, surveys[docId].reward), "Token transfer failed");
        //�o�e���}���y (from contract )
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
    // �����ݨ�
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
       if(openUserCount[docId]!=surveys[docId].publishQty){  //�n�k�٤��e�w�g���ڭn?�����}��ƪ��H�����y  (�����o��ƶq-�w���}�ƶq)*���}���y
            uint256 notOpenCount=surveys[docId].publishQty-openUserCount[docId];
            returnOpenReward=notOpenCount*surveys[docId].openReward;

            
            require(transferFromContract(surveys[docId].creator, returnOpenReward), "Token transfer failed");

       }
       uint256 returnReward=0;
        if(respondents.length!=surveys[docId].publishQty){ //�k�٨S���񵪪����y
           
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