// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class item : MonoBehaviour
// {
//     public enum ItemPriority
//     {
//         Low,
//         Medium,
//         High
//     }

//     // public class Item
//     // {
//     //     public string itemName;
//     //     public ItemPriority priority;
//     // }

//     public static ItemPriority GetItemsByPriority(string itemName, int priority)
//     {
//         Item newItem = new Item();
//         newItem.itemName = itemName;
//         newItem.priority = priority;
//         return newItem;
//     }

//     public List<Item> items = new List<Item>();

//     // private void NewItem(){

//     //     // Tạo một đối tượng Item mới
//     //     Item newItem = new Item();
//     //     newItem.itemName = "Tips";
//     //     newItem.priority = ItemPriority.High;

//     //     // Thêm đối tượng Item vào danh sách items
//     //     items.Add(newItem);

//     // }

//     public List<Item> CollectAndSortItems()
//     {
//         List<Item> sortedItems = new List<Item>();

//         sortedItems.Add(GetItemsByPriority("Tips",ItemPriority.High));
//         sortedItems.Add(GetItemsByPriority("Tips 1",ItemPriority.Medium));
//         sortedItems.Add(GetItemsByPriority("Tips 2",ItemPriority.Low));

//         sortedItems = sortedItems.OrderBy(item => item.priority).ToList();

//         return sortedItems;
//     }

//     private List<Item> GetItemsByPriority(ItemPriority priority)
//     {
//         List<Item> result = new List<Item>();

//         foreach (var item in items)
//         {
//             if (item.priority == priority)
//             {
//                 result.Add(item);
//             }
//         }
//         return result;
//     }
// }
