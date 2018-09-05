using System.Collections.Generic;

public class Item_Fuego : ItemController
{
    public override void FireAction(AvatarController player)
    {
        if (player.Inventory.Items.Contains("valdeConAgua") == true)
        {
            player.Inventory.Items.Remove("valde");

            Destroy(this.gameObject);
        }
    }
}