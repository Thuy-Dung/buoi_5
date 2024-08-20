// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.18;
import "./Item.sol";
import "./Ownable.sol";
contract ItemManager is Ownable{
  enum SupplyChainStep{
    Created,
    Paid,
    Delivered
  }
  struct S_Item{
    address _itemAddress;
    string _itemName;
    uint256 _itemPrice;
    SupplyChainStep _step;
  }
  uint256 itemIndex;
  mapping(uint256 => S_Item) public items;
  event ItemUpdated(uint256 itemIndex, SupplyChainStep step);
function createItem(string memory _itemName, uint256 _itemPrice) public onlyOwner{
    // Tạo một đối tượng mới 'Item' với các tham số đầu vào là địa chỉ của chủ sở hữu, chỉ số của mặt hàng và giá của mặt hàng.
    Item item = new Item(this, itemIndex, _itemPrice);

    // Thêm mặt hàng mới vào danh sách các mặt hàng. Mặt hàng này có địa chỉ, tên, giá và trạng thái là 'Created'.
    items[itemIndex] = S_Item(
      address(item),
      _itemName,
      _itemPrice,
      SupplyChainStep.Created
    );

    // Phát sự kiện 'ItemUpdated' với chỉ số của mặt hàng và trạng thái 'Created'.
    emit ItemUpdated(itemIndex, SupplyChainStep.Created);

    // Tăng chỉ số của mặt hàng lên 1 để chuẩn bị cho việc tạo mặt hàng tiếp theo.
    itemIndex++;
}

function triggerPayment(uint256 _itemIndex) public payable{
    // Kiểm tra xem số tiền gửi đi có bằng với giá của mặt hàng hay không. 
    // Nếu không, hàm sẽ bị hủy và thông báo lỗi "Only full payments accepted" sẽ được hiển thị.
    require(
      items[_itemIndex]._itemPrice == msg.value,
      "Only full payments accepted"
    );

    // Kiểm tra xem mặt hàng đã được tạo hay chưa. Nếu mặt hàng đã vượt qua giai đoạn 'Created', hàm sẽ bị hủy và thông báo lỗi "Item is further in the chain" sẽ được hiển thị.
    require(
      items[_itemIndex]._step == SupplyChainStep.Created,
      "Item is further in the chain"
    );

    // Cập nhật trạng thái của mặt hàng thành 'Paid'.
    items[_itemIndex]._step = SupplyChainStep.Paid;

    // Phát sự kiện 'ItemUpdated' với chỉ số của mặt hàng và trạng thái 'Paid'.
    emit ItemUpdated(_itemIndex, SupplyChainStep.Paid);
}


function triggerDelivery(uint256 _itemIndex) public onlyOwner{
    // Kiểm tra xem mặt hàng đã được thanh toán hay chưa. 
    // Nếu mặt hàng đã vượt qua giai đoạn 'Paid', hàm sẽ bị hủy và thông báo lỗi "Item is further in the chain" sẽ được hiển thị.
    require(
      items[_itemIndex]._step == SupplyChainStep.Paid,
      "Item is further in the chain"
    );

    // Cập nhật trạng thái của mặt hàng thành 'Delivered'.
    items[_itemIndex]._step = SupplyChainStep.Delivered;

    // Phát sự kiện 'ItemUpdated' với chỉ số của mặt hàng và trạng thái 'Delivered'.
    emit ItemUpdated(_itemIndex, SupplyChainStep.Delivered);
}

}