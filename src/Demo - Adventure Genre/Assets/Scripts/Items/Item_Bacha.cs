using System.Collections.Generic;

public class Item_Bacha : ItemController
{
    public override void FireAction(AvatarController player)
    {
        if (player.Inventory.Items.Contains("valde") == true)
        {
            player.Inventory.Items.Remove("valde");

            player.ActiveInventory_Valde.SetActive(false);
            player.ActiveInventory_ValdeConAgua.SetActive(true);

            player.Inventory.Items.Add("valdeConAgua");
        }
    }
}