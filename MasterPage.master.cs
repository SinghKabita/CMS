using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using System.IO;
using System.Collections;
using System.Web.UI.HtmlControls;
using Entity.Framework;
using DataHelper.Framework;

public partial class MasterPage : System.Web.UI.MasterPage
{
    ArrayList alist = new ArrayList();

    Modules MODEnt = new Modules();
    ModulesService MODSer = new ModulesService();

    SubModules SUBEnt = new SubModules();
    SubModulesService SUBSer = new SubModulesService();

    Pages PGEnt = new Pages();
    PagesService PGSer = new PagesService();

    Entity.Components.Login LEnt = new Entity.Components.Login();
    LoginService LSer = new LoginService();

    HelperFunction hf = new HelperFunction();
    UserProfileEntity userProfileEnt = new UserProfileEntity();

    Boolean IsPageRefresh = false;

    EntityList thelist = new EntityList();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();

        try
        {
            logoClick();
            string imgfolder;
            EntityList commentList = new EntityList();
            EntityList Replylist = new EntityList();
            EntityList newlist = new EntityList();
            userProfileEnt = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
            LEnt = new Entity.Components.Login();
            LEnt.EMPLOYEEID = userProfileEnt.EmployeeID.ToString();
            LEnt = (Entity.Components.Login)LSer.GetSingle(LEnt);


            repeaterModule.DataSource = hf.getModule(userProfileEnt.UserGroupID.ToString());
            repeaterModule.DataBind();



            imgfolder = Server.MapPath(@"~/images/user/") + userProfileEnt.EmployeeID + ".jpg";
            if (File.Exists(imgfolder))
            {
                imgprofile.ImageUrl = @"~/images/user/" + userProfileEnt.EmployeeID + ".jpg";

            }
            else
            {

                imgprofile.ImageUrl = @"~/images/user/male.jpg";
            }
            
        }
        catch(Exception ex)
        {
            Response.Redirect("~/Login.aspx");
        }



    }

    protected void logoClick()
    {
        hlinkLogo.NavigateUrl = "~/Home.aspx";


    }


    protected string SetCssClass(string page)
    {
        return Request.Url.AbsolutePath.ToLower().EndsWith(page.ToLower()) ? "active-menu" : "";
    }

    protected void linkSignout_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        Session.Clear();
        Session.RemoveAll();
        Response.Redirect("~/Login.aspx");
    }

    protected void repeaterModule_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblModuleId = e.Item.FindControl("lblModuleId") as Label;
        Repeater repeaterSubModule = e.Item.FindControl("repeaterSubModule") as Repeater;
        Label lblIcon = e.Item.FindControl("lblIcon") as Label;

        //HtmlGenericControl menuicon = new HtmlGenericControl("menuicon");
        //menuicon.Attributes.Add("class", "fa fa-ambulance");


        HtmlGenericControl menuicon = (HtmlGenericControl)e.Item.FindControl("menuicon");
        menuicon.Attributes["class"] = lblIcon.Text;



        repeaterSubModule.DataSource = hf.getSubModule(userProfileEnt.UserGroupID.ToString(), lblModuleId.Text);
        repeaterSubModule.DataBind();


    }



    protected void repeaterSubModule_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblParentModuleId = e.Item.FindControl("lblParentModuleId") as Label;
        Label lblSubModuleId = e.Item.FindControl("lblSubModuleId") as Label;
        Repeater repeaterPages = e.Item.FindControl("repeaterPages") as Repeater;

        repeaterPages.DataSource = hf.getlinkname(lblParentModuleId.Text, lblSubModuleId.Text, userProfileEnt.UserGroupID.ToString());
        repeaterPages.DataBind();
        string url = HttpContext.Current.Request.Url.AbsolutePath;
        url = url.Replace("/college", "~");

        PGEnt = new Pages();
        PGEnt.PAGENAME = url;
        PGEnt = (Pages)PGSer.GetSingle(PGEnt);
        if (PGEnt != null)
        {
            breadcrumb.Visible = true;
            lblPage.Text = PGEnt.LINKNAME;

            SUBEnt = new SubModules();
            SUBEnt.SUBMODULE_ID = PGEnt.SUBMODULEID;
            SUBEnt = (SubModules)SUBSer.GetSingle(SUBEnt);
            if (SUBEnt != null)
            {
                lblSubModule.Text = SUBEnt.SUBMODULE_NAME;
            }

            MODEnt = new Modules();
            MODEnt.MODULE_ID = PGEnt.SHOWINMODULE;
            MODEnt = (Modules)MODSer.GetSingle(MODEnt);
            if (MODEnt != null)
            {
                lblModule.Text = MODEnt.MODULE_NAME;
            }
        }
        else
        {
            breadcrumb.Visible = false;
        }
    }




}
