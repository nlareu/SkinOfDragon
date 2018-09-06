using System.Collections.Generic;

public class Item_Valde : ItemController
{
    public override void FireAction(AvatarController player)
    {
        if (player.Inventory.Items.Contains("valde") == false)
        {
            player.Inventory.Items.Add("valde");

            player.ActiveInventory_Valde.SetActive(true);

            Destroy(this.gameObject);
        }
    }
}