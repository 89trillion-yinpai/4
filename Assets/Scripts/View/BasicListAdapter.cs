/*
 * * * * This bare-bones script was auto-generated * * * *
 * The code commented with "/ * * /" demonstrates how data is retrieved and passed to the adapter, plus other common commands. You can remove/replace it once you've got the idea
 * Complete it according to your specific use-case
 * Consult the Example scripts if you get stuck, as they provide solutions to most common scenarios
 * 
 * Main terms to understand:
 *		Model = class that contains the data associated with an item (title, content, icon etc.)
 *		Views Holder = class that contains references to your views (Text, Image, MonoBehavior, etc.)
 * 
 * Default expected UI hiererchy:
 *	  ...
 *		-Canvas
 *		  ...
 *			-MyScrollViewAdapter
 *				-Viewport
 *					-Content
 *				-Scrollbar (Optional)
 *				-ItemPrefab (Optional)
 * 
 * Note: If using Visual Studio and opening generated scripts for the first time, sometimes Intellisense (autocompletion)
 * won't work. This is a well-known bug and the solution is here: https://developercommunity.visualstudio.com/content/problem/130597/unity-intellisense-not-working-after-creating-new-1.html (or google "unity intellisense not working new script")
 * 
 * 
 * Please read the manual under "Assets/OSA/Docs", as it contains everything you need to know in order to get started, including FAQ
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using frame8.Logic.Misc.Other.Extensions;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using UnityEngine.Serialization;
using SimpleJSON;

// You should modify the namespace to your own or - if you're sure there won't ever be conflicts - remove it altogether
namespace Assets.Scripts
{
	// There are 2 important callbacks you need to implement, apart from Start(): CreateViewsHolder() and UpdateViewsHolder()
	// See explanations below
	public class BasicListAdapter : OSA<BaseParamsWithPrefab, MyListItemViewsHolder>
	{
		// Helper that stores data and notifies the adapter when items count changes
		// Can be iterated and can also have its elements accessed by the [] operator
		public SimpleDataHelper<MyListItemModel> Data { get; private set; }
		//读取json数据列表
		public JsonController list;
		//前三名的背景    "FormerlySerializedAs"防止变量名改变丢失引用
		public Sprite nomalSprite;
		[FormerlySerializedAs("Rank3Sprite")] public Sprite rank3Sprite;
		[FormerlySerializedAs("Rank2Sprite")] public Sprite rank2Sprite;
		[FormerlySerializedAs("Rank1Sprite")] public Sprite rank1Sprite;
		//前三名的奖牌    "FormerlySerializedAs"防止变量名改变丢失引用
		[FormerlySerializedAs("Rank3")] public Sprite rank3;
		[FormerlySerializedAs("Rank2")] public Sprite rank2;
		[FormerlySerializedAs("Rank1")] public Sprite rank1;
		//声明段位图标   "FormerlySerializedAs"防止变量名改变丢失引用
		[FormerlySerializedAs("Stand1onRank")] public Sprite stand1ONRank;
		[FormerlySerializedAs("Stand2onRank")] public Sprite stand2ONRank;
		[FormerlySerializedAs("Stand3onRank")] public Sprite stand3ONRank;
		[FormerlySerializedAs("Stand4onRank")] public Sprite stand4ONRank;
		[FormerlySerializedAs("Stand5onRank")] public Sprite stand5ONRank;
		[FormerlySerializedAs("Stand6onRank")] public Sprite stand6ONRank;
		[FormerlySerializedAs("Stand7onRank")] public Sprite stand7ONRank;
		[FormerlySerializedAs("Stand8onRank")] public Sprite stand8ONRank;
		//对话框
		public GameObject talk;
		//对话框里显示自己名字
		public Text myself;
		//对话框里自己的奖杯数
		public Text cupNum;

		#region OSA implementation
		//初始化
		protected override void Awake()
		{
			Data = new SimpleDataHelper<MyListItemModel>(this);

			// Calling this initializes internal data and prepares the adapter to handle item count changes
			base.Awake();
			//读取json数据
			list.duqu();
			//复用次数等于json数据个数
			RetrieveDataAndUpdate(list.item.Count);
			// Retrieve the models from your data source and set the items count
			/*
			
			*/
		}

		// This is called initially, as many times as needed to fill the viewport, 
		// and anytime the viewport's size grows, thus allowing more items to be displayed
		// Here you create the "ViewsHolder" instance whose views will be re-used
		// *For the method's full description check the base implementation
		protected override MyListItemViewsHolder CreateViewsHolder(int itemIndex)
		{
			var instance = new MyListItemViewsHolder();

			// Using this shortcut spares you from:
			// - instantiating the prefab yourself
			// - enabling the instance game object
			// - setting its index 
			// - calling its CollectViews()
			instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

			return instance;
		}

		// This is called anytime a previously invisible item become visible, or after it's created, 
		// or when anything that requires a refresh happens
		// Here you bind the data from the model to the item's views
		// *For the method's full description check the base implementation
		
		protected override void UpdateViewsHolder(MyListItemViewsHolder newOrRecycled)//更新显示的内容
		{
			// In this callback, "newOrRecycled.ItemIndex" is guaranteed to always reflect the
			// index of item that should be represented by this views holder. You'll use this index
			// to retrieve the model from your data set
			
			MyListItemModel model = Data[newOrRecycled.ItemIndex];
			newOrRecycled.CupNum.text = model.CupNum.ToString();
			newOrRecycled.PlayerName.text = model.PlayerName.ToString();
			if (model.RankStandText < 3)
			{
				//前三名显示特定奖牌，不显示数字
				newOrRecycled.RankStandImage.sprite = model.RankStandImage;
				newOrRecycled.RankStandImage.gameObject.SetActive(true);
				newOrRecycled.RankStandText.gameObject.SetActive(false);
				newOrRecycled.RankStandImage.SetNativeSize();
			}
			else
			{
				//其他人显示数字排名，没有奖牌
				newOrRecycled.RankStandImage.gameObject.SetActive(false);
				newOrRecycled.RankStandText.gameObject.SetActive(true);
				newOrRecycled.RankStandText.text = (model.RankStandText + 1).ToString();
			}
			//每条排行榜添加点击事件
			newOrRecycled.BgButton.GetComponent<Button>().onClick.AddListener(() =>
			{
				//显示对话框
				talk.SetActive(true);
				//显示自己的名字
				myself.text = model.PlayerName;
				//显示自己的奖杯数
				cupNum.text = model.CupNum.ToString();
			});
			//每条排行榜添加点击事件
			/*newOrRecycled.BgButton.GetComponent<Button>().onClick.AddListener(() =>
			{
				buttonPanel.SetActive(true);
				playerString.text = model.PlayerName;
				cupNum.text = model.CupNum.ToString();
				Debug.Log("User:  " + model.PlayerName + "Rank" + (model.Count + 1));
			});*/
			//背景图片
			newOrRecycled.BgButton.GetComponent<Image>().sprite = model.BgButton;
			//段位等级图片
			newOrRecycled.RankGrade.GetComponent<Image>().sprite = model.RankGrade;
			
		}

		// This is the best place to clear an item's views in order to prepare it from being recycled, but this is not always needed, 
		// especially if the views' values are being overwritten anyway. Instead, this can be used to, for example, cancel an image 
		// download request, if it's still in progress when the item goes out of the viewport.
		// <newItemIndex> will be non-negative if this item will be recycled as opposed to just being disabled
		// *For the method's full description check the base implementation
		/*
		protected override void OnBeforeRecycleOrDisableViewsHolder(MyListItemViewsHolder inRecycleBinOrVisible, int newItemIndex)
		{
			base.OnBeforeRecycleOrDisableViewsHolder(inRecycleBinOrVisible, newItemIndex);
		}
		*/

		// You only need to care about this if changing the item count by other means than ResetItems, 
		// case in which the existing items will not be re-created, but only their indices will change.
		// Even if you do this, you may still not need it if your item's views don't depend on the physical position 
		// in the content, but they depend exclusively to the data inside the model (this is the most common scenario).
		// In this particular case, we want the item's index to be displayed and also to not be stored inside the model,
		// so we update its title when its index changes. At this point, the Data list is already updated and 
		// shiftedViewsHolder.ItemIndex was correctly shifted so you can use it to retrieve the associated model
		// Also check the base implementation for complementary info
		/*
		protected override void OnItemIndexChangedDueInsertOrRemove(MyListItemViewsHolder shiftedViewsHolder, int oldIndex, bool wasInsert, int removeOrInsertIndex)
		{
			base.OnItemIndexChangedDueInsertOrRemove(shiftedViewsHolder, oldIndex, wasInsert, removeOrInsertIndex);

			shiftedViewsHolder.titleText.text = Data[shiftedViewsHolder.ItemIndex].title + " #" + shiftedViewsHolder.ItemIndex;
		}
		*/
		#endregion

		// These are common data manipulation methods
		// The list containing the models is managed by you. The adapter only manages the items' sizes and the count
		// The adapter needs to be notified of any change that occurs in the data list. Methods for each
		// case are provided: Refresh, ResetItems, InsertItems, RemoveItems
		#region data manipulation
		public void AddItemsAt(int index, IList<MyListItemModel> items)
		{
			// Commented: the below 2 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
			//YourList.InsertRange(index, items);
			//InsertItems(index, items.Length);

			Data.InsertItems(index, items);
		}

		public void RemoveItemsFrom(int index, int count)
		{
			// Commented: the below 2 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
			//YourList.RemoveRange(index, count);
			//RemoveItems(index, count);

			Data.RemoveItems(index, count);
		}

		public void SetItems(IList<MyListItemModel> items)
		{
			// Commented: the below 3 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
			//YourList.Clear();
			//YourList.AddRange(items);
			//ResetItems(YourList.Count);

			Data.ResetItems(items);
		}
		#endregion


		// Here, we're requesting <count> items from the data source
		void RetrieveDataAndUpdate(int count)
		{
			StartCoroutine(FetchMoreItemsFromDataSourceAndUpdate(count));
		}

		// Retrieving <count> models from the data source and calling OnDataRetrieved after.
		// In a real case scenario, you'd query your server, your database or whatever is your data source and call OnDataRetrieved after
		IEnumerator FetchMoreItemsFromDataSourceAndUpdate(int count)
		{
			// Simulating data retrieving delay
			
			yield return new WaitForSeconds(0.5f);
			var newItems = new MyListItemModel[count];
			

			// Retrieve your data here
			for (int i = 0; i < count; ++i)
			{
				var model = new MyListItemModel()
				{
					//玩家名字
					PlayerName = list.item[i].nickName,
					//玩家拥有奖杯数
					CupNum = list.item[i].trophy,
					//设置i的值下面区分前三名和其他人的时候用
					Count = i,
				};
				//根据奖杯数量判断显示段位
                if ((list.item[i].trophy ) < 1000)
                {
                    model.RankGrade = stand8ONRank;
                }
                else if ((list.item[i].trophy ) < 2000)
                {
                    model.RankGrade = stand7ONRank;
                }
                else if ((list.item[i].trophy ) < 3000)
                {
                    model.RankGrade = stand6ONRank;
                }
                else if ((list.item[i].trophy ) < 4000)
                {
                    model.RankGrade = stand5ONRank;
                }
                else if ((list.item[i].trophy ) < 5000)
                {
                    model.RankGrade = stand4ONRank;
                }
                else if ((list.item[i].trophy ) < 6000)
                {
                    model.RankGrade = stand3ONRank;
                }
                else if ((list.item[i].trophy ) < 7000)
                {
                    model.RankGrade = stand2ONRank;
                }
                else
                {
                    model.RankGrade = stand1ONRank;
                }
                //前三名设置特定的背景和奖牌
                if (i < 3)
                {
                    if (i == 0)
                    {
                        model.BgButton = rank1Sprite;
                        model.RankStandImage = rank1;
                    }

                    if (i == 1)
                    {
                        model.BgButton = rank2Sprite;
                        model.RankStandImage = rank2;
                    }

                    if (i == 2)
                    {
                        model.BgButton = rank3Sprite;
                        model.RankStandImage = rank3;
                    }
                }
                //其他人显示数字排名而不是奖牌，其他人统一背景颜色
                else
                {
                    model.RankStandText = i;
                    model.BgButton = nomalSprite;
                }
				newItems[i] = model;
			}
			

			OnDataRetrieved(newItems);
		}

		void OnDataRetrieved(MyListItemModel[] newItems)
		{
			Data.InsertItemsAtEnd(newItems);
		}
	}

	// Class containing the data associated with an item
	public class MyListItemModel
	{
		/*
		public string title;
		public Color color;
		*/
		//声明匹配的ui组件内容类型
		public Sprite BgButton;
		public Sprite RankGrade;
		public int CupNum;
		public Sprite RankStandImage;
		public int RankStandText;
		public string PlayerName;
		public int Count;
		//public Sprite head;
	}


	// This class keeps references to an item's views.
	// Your views holder should extend BaseItemViewsHolder for ListViews and CellViewsHolder for GridViews
	public class MyListItemViewsHolder : BaseItemViewsHolder
	{
		/*
		public Text titleText;
		public Image backgroundImage;
		*/
		//声明用到的ui组件类型
		public Image BgButton;
		public Image RankGrade;
		public Text CupNum;
		public Image RankStandImage;
		public Text RankStandText;
		public Text PlayerName;
		//public Image head;
		// GetComponentAtPath is a handy extension method from frame8.Logic.Misc.Other.Extensions
			// which infers the variable's component from its type, so you won't need to specify it yourself
			/*
			root.GetComponentAtPath("TitleText", out titleText);
			root.GetComponentAtPath("BackgroundImage", out backgroundImage);
			*/

			
			// Retrieving the views from the item's root GameObject
		public override void CollectViews()//收集并显示出声明的ui组件
		{
			base.CollectViews();
			root.GetComponentAtPath("BgButton", out BgButton);
			root.GetComponentAtPath("RankGrade", out RankGrade);
			root.GetComponentAtPath("CupNum", out CupNum);
			root.GetComponentAtPath("RankStandImage", out RankStandImage);
			root.GetComponentAtPath("RankStandText", out RankStandText);
			root.GetComponentAtPath("PlayerName", out PlayerName);
			//root.GetComponentAtPath("head", out head);
			// GetComponentAtPath is a handy extension method from frame8.Logic.Misc.Other.Extensions
			// which infers the variable's component from its type, so you won't need to specify it yourself
			/*
			root.GetComponentAtPath("TitleText", out titleText);
			root.GetComponentAtPath("BackgroundImage", out backgroundImage);
			*/
		}

		// Override this if you have children layout groups or a ContentSizeFitter on root that you'll use. 
		// They need to be marked for rebuild when this callback is fired
		/*
		public override void MarkForRebuild()
		{
			base.MarkForRebuild();

			LayoutRebuilder.MarkLayoutForRebuild(yourChildLayout1);
			LayoutRebuilder.MarkLayoutForRebuild(yourChildLayout2);
			YourSizeFitterOnRoot.enabled = true;
		}
		*/
		

		// Override this if you've also overridden MarkForRebuild() and you have enabled size fitters there (like a ContentSizeFitter)
		/*
		public override void UnmarkForRebuild()
		{
			YourSizeFitterOnRoot.enabled = false;

			base.UnmarkForRebuild();
		}
		*/
	}
}
