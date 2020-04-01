using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Proa_Web
{
    public class clsGlobals
    {
        /// <summary>
        /// Tipos de mensajes desplegables
        /// </summary>
        public enum messageTypes
        {
            info,
            error,
            alert,
            confirm
        }

        /// <summary>
        /// Indicadores de búsqueda
        /// </summary>
        public enum BUSCA_ARCA_MEDISYS
        {
            NO,
            SI           
        }

        /// <summary>
        /// Indicadores de búsqueda
        /// </summary>
        public enum PACIENTE_MANUAL
        {
            NO,
            SI
        }

        /// <summary>
        /// Tipos de Teléfonos
        /// </summary>
        public enum TIPO_TELEFS
        {
            CASA = 1,
            TRABAJO = 2,
            CELULAR = 3,
            URGENCIA = 4

        }

        /// <summary>
        /// Lista de Sexos de personas
        /// </summary>
        public const string SEXO_M = "M";
        public const string SEXO_F = "F";
        public const string SEXO_O = "O";

        /// <summary>
        /// Nombre para identificar el valor del QueryString recibido
        /// </summary>
        /// <remarks></remarks>
        //public const string SESS_QUERY_STRING = "Sess_QueryString";    
        public const string QSID_REPSERV = "QS_RepServ";
        public const string QSID_CARPETRAB = "QS_CarpetaTrabajo";
        public const string QSID_REPSPATH = "QS_ReportsPath";
        public const string QSID_REPNAME = "QS_Reporte";
        public const string QS_VARS = "QS_variables_Reporte";
        public const string QS_PARAMS = "QS_Parametros_Reporte";
        public const string QS_SEPARADOR_PARAMS = "&";
        public const string QS_NOMPAR_VARS = "QS_vars";
        public const string QS_NOMPAR_PARAMS = "QS_pars";

        // Nombres de variables de sessión
        public const string SS_NOM_DETSESIONES = "SS_DetSess";
        public const string SS_NOM_DETESCENDOMIC = "SS_DetEscenDomic";
        public const string SS_NOM_LISTAPACS = "SS_ListaPacs";

        // Texto genérico de error
        public const string ERR_MESAGE = "Ocurrió un error, por favor contacte al CGI local: ";
        // Nombre del comando de edición de los grids
        public const string GRD_EDIT = "EDITAR";
        /// <summary>
        /// Texto identificador para la confirmación de búsquedas en ARCA/MEDISYS
        /// </summary>
        public const string CONFIRM_BUSQ_ARCA_MEDISYS = "BUSQARCAMEDISYS";

        /// <summary>
        /// Obtiene el nombre completo del usuario activo
        /// </summary>
        public static string NombreUsuarioActivo
        {
            get
            {
                if (HttpContext.Current.Session["fullName"] != null)
                {
                    return HttpContext.Current.Session["fullName"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                HttpContext.Current.Session["fullName"] = value;
            }
        }

        public static string codUsuarioActivo
        {
            get
            {
                return "dcruza";
                //if (HttpContext.Current.Session["userName"] != null)
                //{
                //    return HttpContext.Current.Session["userName"].ToString();                    
                //}
                //else
                //{
                //    return string.Empty;
                //}
            }
            set
            {
                HttpContext.Current.Session["userName"] = value;
            }
        }

        public static string cdu
        {
            get
            {
                if (HttpContext.Current.Session["cdu"] != null)
                {
                    return HttpContext.Current.Session["cdu"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                HttpContext.Current.Session["cdu"] = value;
            }
        }

        /// <summary>
        /// Lista de reportes del sistema
        /// </summary>
        public enum ReportesTrabSocialExped
        {
            rptConsultaPacs = 1,
            rptTrabAdmin = 2,
            rptEscenaDom = 3,
            rptInforme42 = 4,
            rptSesiones = 5
        }

        /// <summary>
        /// Mostrar diálogo de impresión de reportes
        /// </summary>
        /// <param name="Page">Página que muestra el diálogo de impresión</param>
        /// <param name="Reporte">Reporte a mostrar</param>
        /// <param name="Parametros">Parámetros del reporte</param>
        /// <param name="IsPortrait">Indica si es portrait o landscape</param>
        /// <param name="Sistema">Sistema actual</param>
        /// <param name="conMargenes">Indica si debe incluir márgenes</param>
        public static void MostrarDialogoImpresion(System.Web.UI.Page Page, string Reporte, Dictionary<string, string> Parametros, bool IsPortrait, bool conMargenes)
        {
            Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), Reporte, MostrarDialogoImpresionModal(Reporte, Parametros, Page));
        }

        /// <summary>
        /// Muestra el diálogo de impresión de reportes con una ventana tipo Modal
        /// </summary>
        /// <param name="pReporte">Reporte solicitado</param>
        /// <param name="Parametros">Parámetros del reporte</param>
        /// <param name="page">Página contenedora</param>
        /// <returns>Script a ejecutar para mostrar la ventana modal</returns>
        private static string MostrarDialogoImpresionModal(string pReporte, Dictionary<string, string> Parametros, System.Web.UI.Page page)
        {
            StringBuilder scriptEjecutar = new StringBuilder();
            StringBuilder VariablesReporte = new StringBuilder();
            string width = ConfigurationManager.AppSettings["modalWidth"];
            string heigth = ConfigurationManager.AppSettings["modalHeigth"];

            // Construir el string para enviar las variables del reporte
            VariablesReporte.Append(QSID_REPSERV + "=" + ReportServerUrl);
            VariablesReporte.Append(QS_SEPARADOR_PARAMS + QSID_CARPETRAB + "=" + CarpetaTrabajo());
            VariablesReporte.Append(QS_SEPARADOR_PARAMS + QSID_REPSPATH + "=" + ReportsPath);
            VariablesReporte.Append(QS_SEPARADOR_PARAMS + QSID_REPNAME + "=" + pReporte);

            // scriptEjecutar.AppendLine("<head></head>");
            //scriptEjecutar.AppendLine("<body>");

            scriptEjecutar.AppendLine("<script type='text/javascript' lang='javascript'>");
            scriptEjecutar.AppendLine("Imprimir()");
            scriptEjecutar.AppendLine("function Imprimir() ");
            scriptEjecutar.AppendLine("{ ");
            // Llamar al webSite de impresión de reportes en modo Modal. Se le envía por QueryString la información necesaria para su ejecución correcta
            scriptEjecutar.AppendLine("    window.open('" + ReportPrintingSiteUrl + "?" + QS_NOMPAR_VARS + "=" + page.Server.UrlEncode(VariablesReporte.ToString()) + "&" + QS_NOMPAR_PARAMS + "=" + page.Server.UrlEncode(ConstruirParametros(Parametros)) + "', 'mywindow', 'location=1,status=1,scrollbars=1,width=" + width + ",height=" + heigth + "');");
            // scriptEjecutar.AppendLine("    window.open('" + ReportPrintingSiteUrl + "?" + QS_NOMPAR_VARS + "=" + page.Server.UrlEncode(VariablesReporte.ToString()) + "&" + QS_NOMPAR_PARAMS + "=" + page.Server.UrlEncode(ConstruirParametros(Parametros)) + "','', 'dialogHeight:" + heigth + "px;dialogWidth:" + width + "px');");
            scriptEjecutar.AppendLine("} ");

            scriptEjecutar.AppendLine("</script> ");
            //scriptEjecutar.AppendLine("</body>");

            return scriptEjecutar.ToString();
        }

        /// <summary>
        /// Construye el string con los parámetros del reporte solicitado
        /// </summary>
        /// <param name="Parametros">Diccionario con los parámetros a utilizar. (-Nombre-, -Valor-)</param>
        /// <returns>String de parámetros concatenados</returns>
        private static string ConstruirParametros(Dictionary<string, string> Parametros)
        {
            StringBuilder sbParams = new StringBuilder();
            Guid alwaysRefreshData = new Guid();

            alwaysRefreshData = Guid.NewGuid();

            // Recorrer la lista de pares
            foreach (KeyValuePair<string, string> par in Parametros)
            {
                // Crear el parámetro para el reporte y pegarlo con los demás
                sbParams.Append("&");
                sbParams.Append(par.Key);
                sbParams.Append("=");
                sbParams.Append(par.Value.Replace("\r\n", " ").Replace("\"", "").Replace("\'", ""));
            }

            // agregar un parámetro de valor aleatorio, para que el reporte refresque siempre los datos.
            sbParams.Append("&GUID=" + alwaysRefreshData.ToString());

            return sbParams.ToString();
        }

        /// <summary>
        /// Obtiene el nombre de la carpeta de trabajo de reportes
        /// </summary>
        /// <param name="sistema"></param>
        /// <returns></returns>
        public static string CarpetaTrabajo()
        {
            return ReportServerWorkFolder;
        }

        /// <summary>
        /// Obtiene la carpeta de trabajo para los reportes y DataSource
        /// </summary>
        public static string ReportServerWorkFolder
        {
            // Se toma desde el config de la Aplicación.
            get { return SafeConfigString("ReportingServices", "CarpetaTrabajo", string.Empty); }
        }

        /// <summary>
        /// URL del site de RS
        /// </summary>
        public static string ReportPrintingSiteUrl
        {
            get { return SafeConfigString("ReportingServices", "ReportPrintingSiteUrl", string.Empty); }
        }

        /// <summary>
        /// Ruta de los reportes en RS
        /// </summary>
        public static string ReportsPath
        {
            get { return SafeConfigString("ReportingServices", "ReportsPath", string.Empty); }
        }

        /// <summary>
        /// Obtiene el URL del Servidor de Reportes de Reporting Services
        /// </summary>
        public static string ReportServerUrl
        {
            // Se toma desde el config de la Aplicación.
            get { return SafeConfigString("ReportingServices", "ReportServerUrl", string.Empty); }
        }

        /// <summary>
        /// Obtiene valores de las secciones del web.config
        /// </summary>
        /// <param name="configSection">Sección a leer</param>
        /// <param name="configKey">Elemento de la sección a obtener</param>
        /// <param name="defaultValue">Valor por defecto</param>
        /// <returns></returns>
        private static string SafeConfigString(string configSection, string configKey, string defaultValue)
        {
            try
            {
                NameValueCollection configSettings = (NameValueCollection)ConfigurationManager.GetSection(configSection);

                string configValue = configSettings[configKey];

                return configValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static int mult5(int num)
        {
            while (num % 5 != 0)
            {
                num += 1;
            }

            if (num == 60)
            {
                return 0;
            }
            else
                return num;
        }

    }
}