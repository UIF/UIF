using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UrbanImpactCommon;

namespace UIF.PerformingArts
{
    public partial class Education : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
            MenuBest = BuildMenu.BuildMenuControl(MenuBest);
            

        }
        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            //+ "&Dept=" + Request.QueryString["Dept"]
        }
        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
            //menucontrol.MenuControlBehavior(e, Request, Response, Request.QueryString["lastname"], Request.QueryString["firstname"]);
        }
    }
}