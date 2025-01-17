// SPDX-License-Identifier: UNLICENSED

pragma solidity ^0.8.18;

contract Ownable{
    address _owner;
    constructor() {
        _owner = msg.sender;
    }
    modifier onlyOwner(){
        require(isOwner(), "You are not the owner");
        _;
    }
    function isOwner() public view returns (bool){
        return(msg.sender == _owner);
    }
}