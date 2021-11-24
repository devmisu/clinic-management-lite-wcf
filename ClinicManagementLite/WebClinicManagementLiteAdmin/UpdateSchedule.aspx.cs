using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class UpdateSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void validateCheck(String list)
        {
            cbxMon.Checked = list.Contains(Util.Constants.MONDAY);
            cbxTue.Checked = list.Contains(Util.Constants.TUESDAY);
            cbxWed.Checked = list.Contains(Util.Constants.WEDNESDAY);
            cbxThu.Checked = list.Contains(Util.Constants.THURSDAY);
            cbxFri.Checked = list.Contains(Util.Constants.FRIDAY);
            cbxSat.Checked = list.Contains(Util.Constants.SATURDAY);
            cbxSun.Checked = list.Contains(Util.Constants.SUNDAY);

        }

    }
}