pragma solidity ^0.8.17;

contract Account{

 event ModifyAccountLog(string userId,string ipfsAddress);

 mapping(string => string) public accounts;

    function CreateAccount(string memory _id, string memory ipfsAddress) public {
        accounts[_id] = ipfsAddress;
        emit ModifyAccountLog(_id,ipfsAddress);
    }

    function GetAccountData(string memory _id) public view returns (string memory) {
        return accounts[_id];
    }

}