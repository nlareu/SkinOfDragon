using System.Collections.Generic;

public class Item_Bacha : ItemController
{
    public override void FireAction(AvatarController player)
    {
        if (player.Inventory.Items.Contains("valde") == true)
        {
            player.Inventory.Items.Remove("valde");

            player.Inventory.Items.Add("valdeConAgua");
        }
    }
}