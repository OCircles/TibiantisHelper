using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TibiantisHelper.DataReader;

namespace TibiantisHelper.Items
{
    class LiquidContainers
    {

        // I didn't feel like this fit into DataReader so I guess we're putting it here..?

        public static LiquidContainer GetLiquidContainerName(NPC.Transaction transaction)
        {
            Item item = DataReader.Items.Where(i => i.ID == transaction.ItemID).FirstOrDefault();

            LiquidContainer container = new LiquidContainer();

            if (item != null)
            {
                switch (transaction.Data)
                {
                    case 1:
                        container.liquid = "Water";
                        container.spriteIndex = 1;
                        break;
                    case 2:
                        container.liquid = "Wine";
                        container.spriteIndex = 7;
                        break;
                    case 3:
                        container.liquid = "Beer";
                        container.spriteIndex = 3;
                        break;
                    case 4:
                        container.liquid = "Mud";
                        container.spriteIndex = 3;
                        break;
                    case 5:
                        container.liquid = "Blood";
                        container.spriteIndex = 2;
                        break;
                    case 6:
                        container.liquid = "Slime";
                        container.spriteIndex = 4;
                        break;
                    case 7:
                        container.liquid = "Oil";
                        container.spriteIndex = 3;
                        break;
                    case 8:
                        container.liquid = "Urine";
                        container.spriteIndex = 5;
                        break;
                    case 9:
                        container.liquid = "Milk";
                        container.spriteIndex = 6;
                        break;
                    case 10:
                        container.liquid = "Manafluid";
                        container.spriteIndex = 7;
                        break;
                    case 11:
                        container.spriteIndex = 2;
                        container.liquid = "Lifefluid";
                        break;
                    case 12:
                        container.liquid = "Lemonade";
                        container.spriteIndex = 5;
                        break;
                }
            }
            return container;
        }

        public struct LiquidContainer
        {
            public string liquid;
            public byte spriteIndex; // Only for vial, not sure if mug even has a bunch of sprites
        }
    }
}
