using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proa_Web.UserControl
{
    public partial class HorasMinutos : System.Web.UI.UserControl
    {
        public string Title
        {
            set
            {
                lblTitulo.Text = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMinutos();

                CargarHoras();
            }
        }

        private void CargarMinutos()
        {
            for (int i = 0; i <= 55; i += 5)
            {
                ddlMM.Items.Add(new ListItem((i).ToString().PadLeft(2, '0'), (i).ToString().PadLeft(2, '0')));
            }
        }

        private void CargarHoras()
        {
            for (int i = 0; i <= 12; i++)
            {
                ddlHH.Items.Add(new ListItem((i).ToString().PadLeft(2, '0'), (i).ToString().PadLeft(2, '0')));
            }
        }

        public void Limpiar()
        {
            ddlHH.SelectedIndex = 0;
            ddlMM.SelectedIndex = 0;          
        }

        public int ObtenerTiempoMinutosTotales()
        {
           return (int.Parse(ddlHH.SelectedValue) * 60) + int.Parse(ddlMM.SelectedValue);
        }

        public void EstablecerValor(TimeSpan dt)
        {
            ddlHH.SelectedValue = dt.Hours.ToString().PadLeft(2, '0');
            ddlMM.SelectedValue = dt.Minutes.ToString().PadLeft(2, '0');
        }

    }
}