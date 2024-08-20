// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.18;

import "./ItemManager.sol";

contract Item{
    bool isPaid;
    uint256 public itemIndex;
    uint256 public priceInWei;
    ItemManager parentContract;
    constructor(
        ItemManager _parentContract,
        uint256 _itemIndex,
        uint256 _priceInWei
    ){
        priceInWei = _priceInWei;
        itemIndex = _itemIndex;
        parentContract = _parentContract;
    }
   receive() external payable{
    // Kiểm tra xem mặt hàng đã được thanh toán hay chưa. Nếu đã thanh toán, 
    // hàm sẽ bị hủy và thông báo lỗi "Item is paid already" sẽ được hiển thị.
    require(!isPaid, "Item is paid already");

    // Kiểm tra xem số tiền gửi đi có bằng với giá của mặt hàng hay không. 
    // Nếu không, hàm sẽ bị hủy và thông báo lỗi "Only full payments allowed" sẽ được hiển thị.
    require(priceInWei == msg.value, "Only full payments allowed");

    // Gọi hàm 'triggerPayment' của hợp đồng cha với số tiền tương ứng và chỉ số của mặt hàng.
    parentContract.triggerPayment{value: msg.value}(itemIndex);

    // Cập nhật trạng thái thanh toán của mặt hàng thành 'true'.
    isPaid = true;
}

    fallback() external payable { }
}