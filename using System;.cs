using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace Senainfo
{
    public partial class Proyectos_Default : System.Web.UI.Page
    {
        public nino SSnino
        {
            get
            {
                if (Session["neo_SSnino"] == null)
                { Session["neo_SSnino"] = new nino(); }
                return (nino)Session["neo_SSnino"];
            }
            set { Session["neo_SSnino"] = value; }
        }

        public Boolean val2
        {
            get { return (Boolean)Session["val2"]; }
            set { Session["val2"] = value; }
        }

        public Boolean val
        {
            get { return (Boolean)Session["val"]; }
            set { Session["val"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["codProyecto"] = ddown002.SelectedValue.ToString();


            //Bloquear los campos de estados de proyecto
            rdoBtnListEstadoProyecto.Enabled = false;

            if (!IsPostBack)
            {
                if (Session["tokens"] == null || ((DataSet)Session["tokens"]).Tables[0].Rows.Count == 0)
                {
                    Response.Redirect("~/logout.aspx");
                }
                else
                {
                    CargaEstadosProyecto();

                    if (!window.existetoken("49795519-1868-467C-8225-AFD99F8D2C27"))
                    {
                        Response.Redirect("~/logout.aspx");
                    }

                    getinstituciones();
                    getdiasatencion();
                    generaAno();
                    if (Request.QueryString["sw"] == "3")
                    {
                        ddown001.SelectedValue = Request.QueryString["codinst"];

                        Resolucionescoll rcoll = new Resolucionescoll();

                        int EstadoProyecto = rcoll.GetEstadoProyecto(Convert.ToInt32(Request.QueryString["codinst"]));

                        rdoBtnListEstadoProyecto.SelectedValue = EstadoProyecto.ToString();

                        getproyectos();
                    }

                    if (Request.QueryString["sw"] == "4")
                    {
                        //funcion_limpiar();
                        buscador_institucion bsc = new buscador_institucion();
                        Resolucionescoll rcoll = new Resolucionescoll();
                        int codinst = bsc.GetCodInstxCodProy(Convert.ToInt32(Request.QueryString["codinst"]));
                        int EstadoProyecto = rcoll.GetEstadoProyecto(Convert.ToInt32(Request.QueryString["codinst"]));

                        rdoBtnListEstadoProyecto.SelectedValue = EstadoProyecto.ToString();

                        ddown001.SelectedValue = Convert.ToString(codinst);
                        getproyectos();
                        ddown002.SelectedValue = Request.QueryString["codinst"];
                        funcion_cargaresolucion();

                    }
                    validatescurity(); //LO ULTIMO DEL LOAD
                }
            }
        }

        private void validatescurity()
        {
            //0DBEB613-6AA1-4E68-BC00-2C836BEA6404 1.5_INGRESAR
            if (!window.existetoken("0DBEB613-6AA1-4E68-BC00-2C836BEA6404"))
            {
                btnGuardar_NEW.Visible = false;
                lnb001.Visible = false;
            }
            //304DAF27-8B43-4CFA-88C3-321A1A693562 1.5_MODIFICAR
            if (!window.existetoken("304DAF27-8B43-4CFA-88C3-321A1A693562"))
            {

            }
        }

        private void generaAno()
        {
            ddown010.Items.Clear();
            ListItem oItem = new ListItem(Convert.ToString(DateTime.Now.Year), Convert.ToString(DateTime.Now.Year));
            ListItem oItem2 = new ListItem(Convert.ToString(DateTime.Now.AddYears(-1).Year), Convert.ToString(DateTime.Now.AddYears(-1).Year));
            ddown010.Items.Add(oItem);
            ddown010.Items.Add(oItem2);
        }

        private void getinstituciones()
        {
            institucioncoll icoll = new institucioncoll();

            DataView dv1 = new DataView(icoll.GetData(Convert.ToInt32(Session["IdUsuario"])));
            ddown001.DataSource = dv1;
            ddown001.DataTextField = "Nombre";
            ddown001.DataValueField = "CodInstitucion";
            dv1.Sort = "Nombre";
            ddown001.DataBind();
        }

        private Boolean adjudicado()
        {
            if (rdo001.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void getproyectos()
        {
            ddown002.Items.Clear();
            int estado = 0;

            if (string.IsNullOrEmpty(rdoBtnListEstadoProyecto.SelectedValue))
            {
                rdoBtnListEstadoProyecto.BackColor = Color.Yellow;
            }
            else
            {
                if (rdoBtnListEstadoProyecto.SelectedValue == null)
                {
                    estado = 0;

                }
                else
                {
                    rdoBtnListEstadoProyecto.BackColor = Color.Empty;
                    estado = Convert.ToInt32(rdoBtnListEstadoProyecto.SelectedValue);
                }
            }


            //if (rdo001.Checked == true)
            //{
            //    estado = 0;
            //}
            //else
            //{
            //    estado = 1;
            //}

            proyectocoll pcoll = new proyectocoll();
            // DataTable dtproy = pcoll.GetProyectoEstado(Convert.ToInt32(ddown001.SelectedValue), Convert.ToInt32(adjudicado()));
            DataTable dtproy = pcoll.GetProyectoEstado(Convert.ToInt32(Session["IdUsuario"]), "V", Convert.ToInt32(ddown001.SelectedValue), estado);
            DataView dv;
            dv = new DataView(dtproy);
            dv.Sort = "CodProyecto";
            ddown002.DataSource = dv;
            ddown002.DataTextField = "Nombre";
            ddown002.DataValueField = "CodProyecto";
            ddown002.DataBind();


        }

        private void getdiasatencion()
        {
            parcoll pcoll = new parcoll();
            DataTable dtproy = pcoll.GetparDiasAtencion();

            ddown006.DataSource = dtproy;
            ddown006.DataTextField = "Descripcion";
            ddown006.DataValueField = "CodDiasAtencion";
            ddown006.DataBind();
        }

        //protected void btn001_Click(object sender, EventArgs e)
        //{

        //}

        protected void btn002_Click(object sender, EventArgs e)
        {
            Resolucionescoll rcoll = new Resolucionescoll();
            lblMsgSuccess.Visible = false;
            alertS.Visible = false;
            alertW.Visible = false;
            lblMsgWarning.Visible = false;
            System.Drawing.Color colorCampoObligatorio = System.Drawing.ColorTranslator.FromHtml("#F2F5A9"); //gfontbrevis

            if (txtNumResol.Text.Trim() == "" || ddown003.SelectedValue == "0" ||
                txtFecResol.Text == "" || txtFecConvenio.Text == "" || txtFecInicio.Text == "" ||
                txtFecTermino.Text == "" || txtCoberturas.Text.Trim() == "" || ddown001.SelectedValue == "0" || ddown002.SelectedValue == "0" || ddown002.SelectedValue == "" ||
                (txtMonto.Visible == true && txtMonto.Text.Trim() == ""))
            {

                txtNumResol.BackColor = (txtNumResol.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                ddown003.BackColor = (ddown003.SelectedValue == "0") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtFecResol.BackColor = (txtFecResol.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtFecConvenio.BackColor = (txtFecConvenio.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtFecInicio.BackColor = (txtFecInicio.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtFecTermino.BackColor = (txtFecTermino.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtCoberturas.BackColor = (txtCoberturas.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                ddown001.BackColor = (ddown001.SelectedValue == "0") ? colorCampoObligatorio : System.Drawing.Color.White;
                ddown002.BackColor = (ddown002.SelectedValue == "0") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtMonto.BackColor = (txtMonto.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;

                /*
            if (txtNumResol.Text.Trim() == "") { txtNumResol.BackColor = colorCampoObligatorio; }
            else { txtNumResol.BackColor = System.Drawing.Color.White; }
            if (ddown003.SelectedValue == "0") { ddown003.BackColor = colorCampoObligatorio; }
            else { ddown003.BackColor = System.Drawing.Color.White; }
            if (txtFecResol.Text == "") { txtFecResol.BackColor = colorCampoObligatorio; }
            else { txtFecResol.BackColor = System.Drawing.Color.White; }
            if (txtFecConvenio.Text == "") { txtFecConvenio.BackColor = colorCampoObligatorio; }
            else { txtFecConvenio.BackColor = System.Drawing.Color.White; }
            if (txtFecInicio.Text == "") { txtFecInicio.BackColor = colorCampoObligatorio; }
            else { txtFecInicio.BackColor = System.Drawing.Color.White; }
            if (txtFecTermino.Text == "") { txtFecTermino.BackColor = colorCampoObligatorio; }
            else { txtFecTermino.BackColor = System.Drawing.Color.White; }
            if (txtCoberturas.Text.Trim() == "") { txtCoberturas.BackColor = colorCampoObligatorio; }
            else { txtCoberturas.BackColor = System.Drawing.Color.White; }
            if (ddown001.SelectedValue == "0") { ddown001.BackColor = colorCampoObligatorio; }
            else { ddown001.BackColor = System.Drawing.Color.White; }
            if (ddown002.SelectedValue == "0") { ddown002.BackColor = colorCampoObligatorio; }
            else { ddown002.BackColor = System.Drawing.Color.White; }
            if (txtMonto.Text.Trim() == "") { txtMonto.BackColor = colorCampoObligatorio; }
            else { txtMonto.BackColor = System.Drawing.Color.White; }
            */

                alertW.Visible = true;
                lblMsgWarning.Visible = true;

            }
            else
            {

                int nplazasad = (txtPlazasAdic.Text.Trim() != "") ? Convert.ToInt32(txtPlazasAdic.Text) : 0;
                int perplazas = (txtPorcPlazasAsignadas.Text.Trim() != "") ? Convert.ToInt32(txtPorcPlazasAsignadas.Text) : 0;
                int monto = (txtMonto.Text.Trim() != "") ? Convert.ToInt32(txtMonto.Text) : 0;
                int msubatencion = (txtMesSubAtencion.Text.Trim() != "") ? Convert.ToInt32(txtMesSubAtencion.Text) : 0;
                int netapas = (txtNumEtapas.Text.Trim() != "") ? Convert.ToInt32(txtNumEtapas.Text) : 0;

                /*
            int nplazasad = 0;
            if (txtPlazasAdic.Text.Trim() != "") { nplazasad = Convert.ToInt32(txtPlazasAdic.Text); }
            int perplazas = 0;
            if(txtPorcPlazasAsignadas.Text.Trim() !=""){ perplazas=Convert.ToInt32(txtPorcPlazasAsignadas.Text); }
            int monto = 0;
            if(txtMonto.Text.Trim() != "") {monto = Convert.ToInt32(txtMonto.Text);}
            int msubatencion = 0;
            if (txtMesSubAtencion.Text.Trim() != "") { msubatencion = Convert.ToInt32(txtMesSubAtencion.Text); }
            int netapas = 0;
            if (txtNumEtapas.Text.Trim() != "") { netapas = Convert.ToInt32(txtNumEtapas.Text); }
            */
                DateTime FechaTermino = Convert.ToDateTime("01-01-1900").Date;
                if (txtFecTermino.Text != "")
                {
                    FechaTermino = Convert.ToDateTime(txtFecTermino.Text);
                }
                int CodProyecto = Convert.ToInt32(ddown002.SelectedValue);

                rcoll.Insert_Resoluciones(
                    CodProyecto,
                    Convert.ToInt32(ddown010.SelectedValue),
                    txtNumResol.Text.ToUpper(),
                    Convert.ToInt32(ddown001.SelectedValue),
                    Convert.ToInt32(ddown006.SelectedValue),
                    Convert.ToDateTime(txtFecResol.Text),
                    txtMateria.Text.ToUpper(),
                    Convert.ToDateTime(txtFecConvenio.Text),
                    Convert.ToDateTime(txtFecInicio.Text),
                    Convert.ToDateTime(txtFecTermino.Text),
                    netapas,
                    Convert.ToInt32(txtCoberturas.Text.Trim()),
                    nplazasad,
                    perplazas,
                    ddown003.SelectedValue,
                    ddown004.SelectedValue,
                    msubatencion,
                    ddown008.SelectedValue,
                    monto,
                    "V",
                    DateTime.Now,
                    Convert.ToInt32(Session["IdUsuario"])/*usr*/);


                //rcoll.update_proyecto_resolucion(CodProyecto, FechaTermino, Convert.ToInt32(txtCoberturas.Text));

                if (rdo001.Checked == true)
                {
                    //rcoll.Update_EstadoProyecto(CodProyecto);
                    rdo002.Checked = true;
                    rdo001.Checked = false;
                    funcion_cargaresolucion();
                }
                lblMsgSuccess.Visible = true;
                alertS.Visible = true;

                funcion_limpiar();
            }
        }

        private string vigencia()
        {
            string dtime;

            if (Convert.ToDateTime(txtFecTermino.Text).Date <= DateTime.Now.Date)
            {
                dtime = Convert.ToString('V');
            }
            else
            {
                dtime = Convert.ToString('C');
            }
            return dtime;
        }

        private string sexo()
        {
            string sexo;
            sexo = Convert.ToString('M');

            return sexo;
        }

        protected void ddown001_SelectedIndexChanged(object sender, EventArgs e)
        {
            getproyectos();

            //if (Session["codProyecto"] != null && Session["codProyecto"] != "")
            //{
            //    ddown002.SelectedValue = Session["codProyecto"].ToString();
            //}
        }

        protected void ddown003_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGuardar_NEW.Visible = true;
            if (ddown003.SelectedValue == "R")
            {
                txtMateria.Text = "Se readjudica licitación";
                txtMateria.BackColor = Color.White;

                ddown004.Enabled = false;

                DataTable dt = GetResolucionxIcod(ObtenerUltimoICodResolucion());

                ActualizarFechas(dt);
            }
            else
            {
                txtMateria.Text = string.Empty;
                txtMateria.BackColor = Color.LightYellow;
                ddown004.Enabled = true;


                cale_txtFecResol.StartDate = new DateTime(1990, 01, 01);
                cale_txtFecResol.EndDate = DateTime.Now.AddYears(5);

                cale_txtFecConvenio.StartDate = new DateTime(1990, 01, 01);
                cale_txtFecConvenio.EndDate = DateTime.Now.AddYears(5);

                cale_txtFecTermino.StartDate = new DateTime(1990, 01, 01);
                cale_txtFecTermino.EndDate = DateTime.Now.AddYears(5);

            }
        }

        protected void rdo001_CheckedChanged(object sender, EventArgs e)
        {
            string dd1value = ddown001.SelectedValue;
            funcion_limpiar();
            ddown001.SelectedValue = dd1value;
            if (ddown001.SelectedValue != "0")
            {
                getproyectos();
            }
        }

        protected void rdo002_CheckedChanged(object sender, EventArgs e)
        {
            string dd1value = ddown001.SelectedValue;
            funcion_limpiar();
            ddown001.SelectedValue = dd1value;
            if (ddown001.SelectedValue != "0")
            {
                getproyectos();
            }
        }

        protected void ddown002_SelectedIndexChanged(object sender, EventArgs e)
        {
            funcion_cargaresolucion();
            rdoBtnListEstadoProyecto.BackColor = Color.Empty;
        }

        private DataTable ObtenerTiposResolucion(int codEstadoProyecto)
        {
            Conexiones c = new Conexiones();

            List<SqlParameter> list = new List<SqlParameter>();

            list.Add(new SqlParameter("@EstadoProyecto", codEstadoProyecto));

            return c.EjecutaSpToDataTable("GetTiposResolucion", list);
        }

        private void funcion_cargaresolucion()
        {
            Resolucionescoll rcoll = new Resolucionescoll();
            int tiposubvencion = rcoll.GetTipoSubvencionxProyecto(Convert.ToInt32(ddown002.SelectedValue));
            if (tiposubvencion == 12)
            {
                lbl002.Visible = true;
                txtMonto.Visible = true;
                trlbl002.Visible = true;
            }
            else
            {
                lbl002.Visible = false;
                txtMonto.Visible = false;
                trlbl002.Visible = false;
            }

            var estadoProyecto = Convert.ToInt32(rdoBtnListEstadoProyecto.SelectedValue);
            CargaTipoResolucion(estadoProyecto);

            DataView dv1 = new DataView(rcoll.GetTipoResolucionxProyInst(Convert.ToInt32(ddown002.SelectedValue), Convert.ToInt32(ddown001.SelectedValue)));

            if (dv1.Table.Rows.Count > 0)
            {
                txtNumResol.Visible = false;
                ddown007.Visible = true;
                //dv1.Sort = 
                ddown007.DataSource = dv1;
                ddown007.DataTextField = "NumeroResolucion";
                ddown007.DataValueField = "ICodResolucion";
                //dv1.Sort = "NumeroResolucion";
                ddown007.DataBind();

                DataTable dt = GetResolucionxIcod(Convert.ToInt32(ddown007.SelectedValue));

                buscaresolucion(dt);

                btnGuardar_NEW.Visible = false;
            }
            bloquea_dias();
        }

        private void CargaTipoResolucion(int value)
        {


            if (rdoBtnListEstadoProyecto.SelectedValue == null)
            {
                ddown003.Items.Add(new ListItem("Seleccionar", "0"));
            }
            else
            {
                //ddown003.Items.Clear();
                //ddown003.ClearSelection();
                var dt = ObtenerTiposResolucion(value);
                //var dt = ObtenerTiposResolucion(-1);

                ddown003.DataSource = dt;
                ddown003.DataTextField = "Descripcion";
                ddown003.DataValueField = "TipoResolucion";
                ddown003.DataBind();

            }
        }

        private void bloquea_dias()
        {
            proyectocoll pcol = new proyectocoll();
            DataTable dt = pcol.GetProyectos(ddown002.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][10].ToString() == "15")
                {
                    ddown006.SelectedValue = "1";
                    ddown006.Enabled = false;
                }
                else
                {
                    ddown006.Enabled = true;
                }
            }
        }

        protected void ddown007_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = GetResolucionxIcod(Convert.ToInt32(ddown007.SelectedValue));
            buscaresolucion(dt);
        }

        private void UpdateCalendarFechaConvenio(DateTime? date)
        {
            if (ddown003.SelectedValue == "R")
            {
                cale_txtFecConvenio.StartDate = Convert.ToDateTime(hdfFechaTerminoUltimaResolucion.Value);
            }
            else
            {
                cale_txtFecConvenio.StartDate = date;
            }
        }

        private void buscaresolucion(DataTable dt)
        {
            CargaTipoResolucion(-1); //Todas

            Resolucionescoll rcoll = new Resolucionescoll();
            //DataTable dt = rcoll.GetResolucionxIcod(Convert.ToInt32(ddown007.SelectedValue));
            DataTable dtResolucionesProyecto = ObtenerResoluciones(Convert.ToInt32(ddown002.SelectedValue));

            grdResolucionesProyecto.DataSource = dtResolucionesProyecto;
            grdResolucionesProyecto.DataBind();
            grdResolucionesProyecto.Visible = true;
            lblSubtituloResoluciones.Visible = true;

            if (dt.Rows.Count > 0)
            {
                lnb001.Visible = true;

                ddown010.Items.Clear();
                ListItem oItem = new ListItem(Convert.ToString(dt.Rows[0][2]), Convert.ToString(dt.Rows[0][2]));
                ddown010.Items.Add(oItem);

                ddown003.SelectedValue = Convert.ToString(dt.Rows[0][15]);
                txtFecResol.Text = Convert.ToString(dt.Rows[0][6]).Substring(0, 10);




                //txtFecResol.Text = txtFecResol.Text.Remove(11);
                //txtFecResol.Text.Replace(" ", "");

                txtMateria.Text = Convert.ToString(dt.Rows[0][7]);
                txtFecConvenio.Text = Convert.ToString(dt.Rows[0][8]).Substring(0, 10);
                //txtFecConvenio.Text = txtFecConvenio.Text.Remove(11);
                //txtFecConvenio.Text.Replace(" ", "");

                txtFecInicio.Text = Convert.ToString(dt.Rows[0][9]).Substring(0, 10);
                //txtFecInicio.Text = txtFecInicio.Text.Remove(11);
                //txtFecInicio.Text.Replace(" ", "");


                //cale_txtFecConvenio.StartDate = 


                try
                {
                    // txtFecTermino.Text =  Convert.ToDateTime(dt.Rows[0][10].ToString()).AddDays(-1).ToShortDateString();
                    txtFecTermino.Text = Convert.ToString(dt.Rows[0][10]).Substring(0, 10);



                }
                catch
                {
                    txtFecTermino.Text = null;
                }



                //txtFecTermino.Text = txtFecTermino.Text.Remove(11);
                //txtFecTermino.Text.Replace(" ", "");

                txtCoberturas.Text = Convert.ToString(dt.Rows[0][12]);
                txtPlazasAdic.Text = Convert.ToString(dt.Rows[0][13]);
                txtPorcPlazasAsignadas.Text = Convert.ToString(dt.Rows[0][14]);
                ddown006.SelectedValue = Convert.ToString(dt.Rows[0][5]);
                txtMonto.Text = Convert.ToString(dt.Rows[0][19]);
                ddown004.SelectedValue = Convert.ToString(dt.Rows[0][16]);
                ddown008.SelectedValue = Convert.ToString(dt.Rows[0][18]);
                txtNumEtapas.Text = Convert.ToString(dt.Rows[0][11]);
                txtMesSubAtencion.Text = Convert.ToString(dt.Rows[0][17]);
                txtNumeroAdjudicacion.Text = Convert.ToString(dt.Rows[0][23]);
                txtNumeroLicitacion.Text = Convert.ToString(dt.Rows[0][24]);

                ddown001.Enabled = false;
                ddown002.Enabled = false;
                rdo001.Enabled = false;
                rdo002.Enabled = false;
                txtNumeroAdjudicacion.Enabled = false;

                btnGuardar_NEW.Visible = true;

                btnLimpiar_NEW.Visible = true;

                ddown010.Enabled = false;
                txtNumResol.ReadOnly = true;
                txtMateria.ReadOnly = true;
                txtCoberturas.ReadOnly = true;
                txtPlazasAdic.ReadOnly = true;
                txtPorcPlazasAsignadas.ReadOnly = true;
                txtMonto.ReadOnly = true;
                txtNumEtapas.ReadOnly = true;

                ddown003.Enabled = false;
                ddown004.Enabled = false;
                ddown006.Enabled = false;
                ddown008.Enabled = false;

                txtFecResol.ReadOnly = true;
                txtFecConvenio.ReadOnly = true;
                txtFecInicio.ReadOnly = true;
                txtFecTermino.ReadOnly = true;

                if (Convert.ToInt32(dt.Rows[0][11]) > 0)
                {
                    grv001.Visible = true;
                    lbl001.Visible = true;
                    trlbl001.Visible = true;

                    DataTable DT2 = rcoll.GetEtapasResolucionxCod(Convert.ToInt32(dt.Rows[0][0]));

                    grv001.DataSource = DT2;
                    grv001.DataBind();
                }
                else
                {
                    grv001.Visible = false;
                    lbl001.Visible = false;
                    trlbl001.Visible = false;
                }

                //deshabilitar campos
                txtFecInicio.Enabled = false;
                ddown004.Enabled = false;
                txtNumeroLicitacion.Enabled = true;
            }
            validatescurity();
        }

        private void funcion_limpiar()
        {
            rdo001.Enabled = true;
            rdo002.Enabled = true;
            ddown002.Items.Clear();
            //btnGuardar_NEW.Visible = false;
            btnLimpiar_NEW.Visible = true;
            txtNumResol.Visible = true;
            ddown007.Visible = false;

            ddown010.Enabled = true;
            txtNumResol.ReadOnly = false;
            txtMateria.ReadOnly = false;
            txtCoberturas.ReadOnly = false;
            txtPlazasAdic.ReadOnly = false;
            txtPorcPlazasAsignadas.ReadOnly = false;
            txtMonto.ReadOnly = false;
            txtNumEtapas.ReadOnly = false;

            ddown001.Enabled = true;
            ddown002.Enabled = true;
            ddown003.Enabled = true;
            ddown004.Enabled = true;

            ddown006.Enabled = true;
            ddown008.Enabled = true;

            txtFecResol.ReadOnly = false;
            txtFecConvenio.ReadOnly = false;
            txtFecInicio.ReadOnly = false;
            txtFecTermino.ReadOnly = false;


            txtNumResol.Text = "";
            txtMateria.Text = "";
            txtCoberturas.Text = "";
            txtPlazasAdic.Text = "";
            txtPorcPlazasAsignadas.Text = "";
            txtMonto.Text = "";
            txtNumEtapas.Text = "";
            txtMesSubAtencion.Text = "";
            txtMonto.Visible = false;
            lbl002.Visible = false;
            lbl003.Visible = false;
            trlbl002.Visible = false;
            ddown001.SelectedIndex = 0;

            if (ddown002.SelectedIndex > -1)
            {
                ddown002.SelectedIndex = 0;
            }
            //if (ddown003.SelectedIndex > -1)
            //{
            //    ddown003.SelectedIndex = 0;
            //}
            ddown004.SelectedIndex = 0;

            ddown006.SelectedIndex = 0;
            if (ddown007.SelectedIndex > -1)
            {
                ddown007.SelectedIndex = 0;
            }
            ddown008.SelectedIndex = 0;

            grv001.Visible = false;
            lbl001.Visible = false;
            trlbl001.Visible = false;

            txtFecResol.Text = string.Empty;
            txtFecConvenio.Text = string.Empty;
            txtFecInicio.Text = string.Empty;
            txtFecTermino.Text = string.Empty;

            ddown010.BackColor = System.Drawing.Color.White;
            txtNumResol.BackColor = System.Drawing.Color.White;
            ddown003.BackColor = System.Drawing.Color.White;
            txtFecResol.BackColor = System.Drawing.Color.White;
            txtFecConvenio.BackColor = System.Drawing.Color.White;
            txtFecInicio.BackColor = System.Drawing.Color.White;
            txtFecTermino.BackColor = System.Drawing.Color.White;
            txtCoberturas.BackColor = System.Drawing.Color.White;

            ddown001.BackColor = System.Drawing.Color.White;
            ddown002.BackColor = System.Drawing.Color.White;

            divPagoUf.Visible = false;
        }

        //protected void WebImageButton3_Click(object sender, EventArgs e)
        //{
        //    nuevaresolucion();
        //}

        private void nuevaresolucion()
        {
            generaAno();
            //ddown003.Items.Clear();
            //ddown003.Items.Add(new ListItem("Seleccionar", "0"));
            //ddown003.Items.Add(new ListItem("Modificación", "M"));
            //ddown003.Items.Add(new ListItem("Término", "T"));
            //ddown003.Items.Add(new ListItem("Urgencia", "U"));

            ddown003.Items.Clear();
            var estadoProyecto = Convert.ToInt32(rdoBtnListEstadoProyecto.SelectedValue);
            CargaTipoResolucion(estadoProyecto);


            lnb001.Visible = false;
            btnLimpiar_NEW.Visible = true;
            btnGuardar_NEW.Visible = false;
            txtNumResol.Visible = true;
            ddown007.Visible = false;
            ddown010.Enabled = true;
            txtNumResol.ReadOnly = false;
            txtMateria.ReadOnly = false;
            txtCoberturas.ReadOnly = false;
            txtPlazasAdic.ReadOnly = false;
            txtPorcPlazasAsignadas.ReadOnly = false;
            txtMonto.ReadOnly = false;
            txtNumEtapas.ReadOnly = false;

            ddown003.Enabled = true;
            ddown004.Enabled = true;

            ddown006.Enabled = true;
            ddown008.Enabled = true;

            txtFecResol.ReadOnly = false;
            txtFecConvenio.ReadOnly = false;
            txtFecInicio.ReadOnly = true;
            txtFecTermino.ReadOnly = false;
            grv001.Visible = false;
            lbl001.Visible = false;
            trlbl001.Visible = false;



            // JOVM - 19/01/2015
            //cal004.MinDate = Convert.ToDateTime(txtFecInicio.Text).AddDays(1);
        } 


        protected int ObtenerUltimoICodResolucion()
        {
            if (grdResolucionesProyecto.Rows.Count > 0)
                return Convert.ToInt32(grdResolucionesProyecto.Rows[grdResolucionesProyecto.Rows.Count - 1].Cells[0].Text);
            else
                return Convert.ToInt32(ddown007.SelectedValue);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = GetResolucionxIcod(ObtenerUltimoICodResolucion());

            ddown003.SelectedValue = "0";
            buscaresolucion(dt);
            nuevaresolucion();
            bloquea_dias();

            //ActualizarFechas(dt);

            //factor UF
            divPagoUf.Visible = true;
        }


        protected void ActualizarFechas(DataTable dt)
        {
            //Asignamos la fecha de la última resolución como StartDate para la nueva resolución a agregar
            var fechaInicioResolucion = Convert.ToDateTime(dt.Rows[0][6].ToString().Substring(0, 10)).AddDays(1);
            var fechaTerminoResolucion = dt.Rows[0]["FechaTermino"].ToString().Substring(0, 10);


            cale_txtFecResol.StartDate = fechaInicioResolucion;
            txtFecResol.Text = fechaInicioResolucion.ToShortDateString();

            hdfFechaTerminoUltimaResolucion.Value = fechaTerminoResolucion;
            txtFecConvenio.Text = fechaTerminoResolucion.ToString();

            cale_txtFecConvenio.StartDate = Convert.ToDateTime(fechaTerminoResolucion);
            cale_txtFecConvenio.EndDate = Convert.ToDateTime(fechaTerminoResolucion);

            cale_txtFecTermino.StartDate = Convert.ToDateTime(fechaTerminoResolucion);

            //UpdateCalendarFechaConvenio(Convert.ToDateTime(dt.Rows[0]["FechaConvenio"].ToString().Substring(0, 10)));
        }

        //protected void lbn_buscar_institucion_Click(object sender, EventArgs e)
        //{
        //    val = false;
        //    val2 = false;
        //    if (rdo001.Checked)
        //    {
        //        val = rdo001.Checked;
        //    }
        //    else
        //    {
        //        val2 = rdo002.Checked;
        //    }

        //    iframe_bsc_institucion.Src = "../mod_instituciones/bsc_institucion.aspx?param001=Plan de Intervencion&dir=../mod_proyectos/proyectoadjudicadoenejecucion.aspx";
        //    iframe_bsc_institucion.Attributes.Add("height", "300px");
        //    iframe_bsc_institucion.Attributes.Add("width", "760px");
        //    iframe_bsc_institucion.Visible = true;
        //    mpe1.Show();
        //}

        //protected void WebImageButton3_Click1(object sender, EventArgs e)
        //{
        //    Response.Redirect("../mod_instituciones/index_instituciones.aspx");
        //}

        //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        //{
        //    val = false;
        //    val2 = false;
        //    if (rdo001.Checked)
        //    {
        //        val = rdo001.Checked;
        //    }
        //    else
        //    {
        //        val2 = rdo002.Checked;
        //    }
        //    string etiqueta = "Plan de Intervencion";
        //    string cadena = string.Empty;
        //    cadena = @"window.open(this.Page, '../mod_instituciones/bsc_institucion.aspx?param001=" + etiqueta + "&dir=../mod_proyectos/proyectoadjudicadoenejecucion.aspx', 'Buscador', false, true, '770', '420', false, false, true)";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", cadena, true);
        //}

        //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        //{
        //    string etiqueta = "Busca Proyectos";
        //    string cadena = string.Empty;
        //    cadena = @"window.open(this.Page, '../mod_instituciones/bsc_institucion.aspx?param001=" + etiqueta + "&dir=../mod_proyectos/proyectoadjudicadoenejecucion.aspx', 'Buscador', false, true, '770', '420', false, false, true)";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", cadena, true);
        //}

        /*-----------------------------------------------------------------------------------------
    // 15/01/2015
    // Se agregan las siguientes VOID que reemplazarán a los originales que se encuentran 
    // asociados a Infragistics.
    // Se usa el mismo nombre de las VOID originales y se agrega el sufijo "NEW"
    // para su diferenciación.
    //-----------------------------------------------------------------------------------------*/

        //protected void btnVolver_Click_NEW(object sender, EventArgs e)
        //{
        //    Response.Redirect("../mod_instituciones/index_instituciones.aspx");
        //}

        protected void btnLimpiar_Click_NEW(object sender, EventArgs e)
        {
            //Response.Redirect("proyectoadjudicadoenejecucion.aspx");
            funcion_limpiar();
        }

        private bool rangoFechas()
        {
            DateTime FecInicio = DateTime.Parse(txtFecInicio.Text);
            DateTime FecTermino = DateTime.Parse(txtFecTermino.Text);
            int result = DateTime.Compare(FecInicio, FecTermino);

            if (result < 0)
            {
                //f1 es anterior a f2
                return true;
            }
            else if (result == 0)
            {
                //f1 es igual que f2
                return true;
            }
            else
            {
                //f1 es posterior a f2
                return false;
            }
        }

        protected void btnGuardar_Click_NEW(object sender, EventArgs e)
        {
            Resolucionescoll rcoll = new Resolucionescoll();
            lblMsgSuccess.Visible = false;
            alertS.Visible = false;
            alertW.Visible = false;
            lblMsgWarning.Visible = false;
            System.Drawing.Color colorCampoObligatorio = System.Drawing.ColorTranslator.FromHtml("#F2F5A9"); //gfontbrevis

            if (txtNumResol.Text.Trim() == "" || ddown003.SelectedValue == "0" ||
                txtFecResol.Text.Trim() == "" || txtFecConvenio.Text.Trim() == "" || txtFecInicio.Text.Trim() == "" ||
                txtFecTermino.Text.Trim() == "" || txtCoberturas.Text.Trim() == "" || ddown001.SelectedValue == "0" || ddown002.SelectedValue == "0" || ddown002.SelectedValue == "" ||
                (txtMonto.Visible == true && txtMonto.Text.Trim() == "") || lb_fi.Visible == true)
            {
                txtNumResol.BackColor = (txtNumResol.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                ddown003.BackColor = (ddown003.SelectedValue == "0") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtFecResol.BackColor = (txtFecResol.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtFecConvenio.BackColor = (txtFecConvenio.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtFecInicio.BackColor = (txtFecInicio.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtFecTermino.BackColor = (txtFecTermino.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtCoberturas.BackColor = (txtCoberturas.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;
                ddown001.BackColor = (ddown001.SelectedValue == "0") ? colorCampoObligatorio : System.Drawing.Color.White;
                ddown002.BackColor = (ddown002.SelectedValue == "0") ? colorCampoObligatorio : System.Drawing.Color.White;
                txtMonto.BackColor = (txtMonto.Text.Trim() == "") ? colorCampoObligatorio : System.Drawing.Color.White;

                alertW.Visible = true;
                lblMsgWarning.Visible = true;
            }
            else
            {

                int nplazasad = (txtPlazasAdic.Text.Trim() != "") ? Convert.ToInt32(txtPlazasAdic.Text) : 0;
                int perplazas = (txtPorcPlazasAsignadas.Text.Trim() != "") ? Convert.ToInt32(txtPorcPlazasAsignadas.Text) : 0;
                int monto = (txtMonto.Text.Trim() != "") ? Convert.ToInt32(txtMonto.Text) : 0;
                int msubatencion = (txtMesSubAtencion.Text.Trim() != "") ? Convert.ToInt32(txtMesSubAtencion.Text) : 0;
                int netapas = (txtNumEtapas.Text.Trim() != "") ? Convert.ToInt32(txtNumEtapas.Text) : 0;

                bool chkUf = chkUF.Checked;

                DateTime FechaTermino = Convert.ToDateTime("01-01-1900").Date;
                if (txtFecTermino.Text != "")
                {
                    FechaTermino = Convert.ToDateTime(txtFecTermino.Text);
                }
                int CodProyecto = Convert.ToInt32(ddown002.SelectedValue);

                AgregarResolucion(CodProyecto,
                    Convert.ToInt32(ddown010.SelectedValue),
                    txtNumResol.Text.ToUpper(),
                    Convert.ToInt32(ddown001.SelectedValue),
                    Convert.ToInt32(ddown006.SelectedValue),
                    Convert.ToDateTime(txtFecResol.Text),
                    txtMateria.Text.ToUpper(),
                    Convert.ToDateTime(txtFecConvenio.Text),
                    Convert.ToDateTime(txtFecInicio.Text),
                    Convert.ToDateTime(txtFecTermino.Text),
                    netapas,
                    Convert.ToInt32(txtCoberturas.Text.Trim()),
                    nplazasad,
                    perplazas,
                    ddown003.SelectedValue,
                    ddown004.SelectedValue,
                    msubatencion,
                    ddown008.SelectedValue,
                    monto,
                    "V",
                    DateTime.Now,
                    Convert.ToInt32(Session["IdUsuario"]),
                    chkUf,
                    txtNumeroLicitacion.Text
                    );

                //rcoll.Insert_Resoluciones(
                //    CodProyecto, 
                //    Convert.ToInt32(ddown010.SelectedValue), 
                //    txtNumResol.Text.ToUpper(),
                //    Convert.ToInt32(ddown001.SelectedValue), 
                //    Convert.ToInt32(ddown006.SelectedValue), 
                //    Convert.ToDateTime(txtFecResol.Text),
                //    txtMateria.Text.ToUpper(), 
                //    Convert.ToDateTime(txtFecConvenio.Text), 
                //    Convert.ToDateTime(txtFecInicio.Text),
                //    Convert.ToDateTime(txtFecTermino.Text),
                //    netapas,
                //    Convert.ToInt32(txtCoberturas.Text.Trim()), 
                //    nplazasad, 
                //    perplazas, 
                //    ddown003.SelectedValue,
                //    ddown004.SelectedValue, 
                //    msubatencion, 
                //    ddown008.SelectedValue, 
                //    monto, "V", 
                //    DateTime.Now, 
                //    Convert.ToInt32(Session["IdUsuario"])/*usr*/);

                //rcoll.update_proyecto_resolucion(CodProyecto, FechaTermino, Convert.ToInt32(txtCoberturas.Text));

                //ActualizarProyectoResolucion(CodProyecto, FechaTermino, Convert.ToInt32(txtCoberturas.Text));

                if (rdo001.Checked == true)
                {
                    ActualizarEstadoProyecto(CodProyecto);
                    //rcoll.Update_EstadoProyecto(CodProyecto);
                    rdo002.Checked = true;
                    rdo001.Checked = false;
                    funcion_cargaresolucion();
                }
                lblMsgSuccess.Visible = true;
                alertS.Visible = true;

                funcion_limpiar();

                if (ddown002.SelectedValue == "")
                {
                    ObtenerResoluciones(Convert.ToInt32(Request.QueryString["codinst"]));
                }
                else
                {
                    ObtenerResoluciones(Convert.ToInt32(ddown002.SelectedValue));
                }
            }
        }

        private void ActualizarEstadoProyecto(int codProyecto)
        {
            //ToDo: Refactor
            //var con = new Conexiones();
            //string sql = "UPDATE Proyectos SET EstadoProyecto = 1 WHERE CodProyecto =" + codProyecto;
            //con.Ejecutar(sql);

            //List<SqlParameter> list = new List<SqlParameter>();

            //list.Add(new SqlParameter("@CodProyecto", codProyecto));
            //con.EjecutaSP("Update_EstadoProyecto", list);

        }

        private DataTable ActualizarProyectoResolucion(int codProyecto, DateTime fechaTermino, int numeroPlazas)
        {
            Conexiones c = new Conexiones();
            object objFechaTermino = DBNull.Value;

            if (fechaTermino != Convert.ToDateTime("01-01-1900").Date)
            {
                objFechaTermino = fechaTermino;
            }

            var list = new List<SqlParameter>();

            list.Add(new SqlParameter("@CodProyecto", codProyecto));
            list.Add(new SqlParameter("@FechaTermino", objFechaTermino));
            list.Add(new SqlParameter("@NumeroPlazas", numeroPlazas));

            return c.EjecutaSpToDataTable("Update_Proyecto_Resolucion", list);
        }

        private DataTable AgregarResolucion(int codProyecto, int anoResolucion, String numeroResolucion, int codInstitucion, int codDiasAtencion, DateTime fechaResolucion, string descripcion,
            DateTime fechaConvenio, DateTime fechaInicio, DateTime fechaTermino, int etapas, int numeroPlazas, int plazasAdicionales, int porPlazasAsignadasTribunales,
            string tipoResolucion, string tipoTermino, int mesesSubAtencion, string sexo, int monto,
            string indVigencia, DateTime fechaActualizacion, int idUsuarioActualizacion, bool chkUf, string numeroLicitacion)
        {
            Conexiones c = new Conexiones();

            object objFechaTermino = DBNull.Value;

            if (fechaTermino != Convert.ToDateTime("01-01-1900").Date)
            {
                objFechaTermino = fechaTermino;
            };

            List<SqlParameter> list = new List<SqlParameter>();

            list.Add(new SqlParameter("@CodProyecto", codProyecto));
            list.Add(new SqlParameter("@AnoResolucion", anoResolucion));
            list.Add(new SqlParameter("@NumeroResolucion", numeroResolucion));
            list.Add(new SqlParameter("@CodInstitucion", codInstitucion));
            list.Add(new SqlParameter("@CodDiasAtencion", codDiasAtencion));
            list.Add(new SqlParameter("@FechaResolucion", fechaResolucion));
            list.Add(new SqlParameter("@Descripcion", descripcion));
            list.Add(new SqlParameter("@FechaConvenio", fechaConvenio));
            list.Add(new SqlParameter("@FechaInicio", fechaInicio));
            list.Add(new SqlParameter("@FechaTermino", objFechaTermino));
            list.Add(new SqlParameter("@Etapas", etapas));
            list.Add(new SqlParameter("@NumeroPlazas", numeroPlazas));
            list.Add(new SqlParameter("@PlazasAdicionales", plazasAdicionales));
            list.Add(new SqlParameter("@PorPlazasAsignadasTribunales", porPlazasAsignadasTribunales));
            list.Add(new SqlParameter("@TipoResolucion", tipoResolucion));
            list.Add(new SqlParameter("@TipoTermino", tipoTermino));
            list.Add(new SqlParameter("@MesesSubAtencion", mesesSubAtencion));
            list.Add(new SqlParameter("@Sexo", sexo));
            list.Add(new SqlParameter("@Monto", monto));
            list.Add(new SqlParameter("@IndVigencia", indVigencia));
            list.Add(new SqlParameter("@FechaActualizacion", fechaActualizacion));
            list.Add(new SqlParameter("@IdUsuarioActualizacion", idUsuarioActualizacion));
            list.Add(new SqlParameter("@PagoUF", chkUf));
            list.Add(new SqlParameter("@NumeroLicitacion", numeroLicitacion));

            return c.EjecutaSpToDataTable("Insert_Resoluciones", list);
        }

        protected void txtNumResol_TextChanged(object sender, EventArgs e)
        {
            Resolucionescoll rcol = new Resolucionescoll();

            lbl003.Visible = false;
            if (ddown002.SelectedValue == "0" || ddown002.SelectedValue == "" || txtNumResol.Text.Trim() == "")
            {
                if (ddown002.SelectedValue == "0" || ddown002.SelectedValue == "")
                {
                    lbl003.Text = "- Debe seleccionar un proyecto.<br>";
                }
                if (txtNumResol.Text.Trim() == "")
                {
                    lbl003.Text += "- Debe ingresar número de Resolución.";
                }
                lbl003.Visible = true;
            }
            else
            {
                lbl003.Visible = false;
                int val = rcol.CheckResolExiste(txtNumResol.Text.Trim(), Convert.ToInt32(ddown002.SelectedValue), Convert.ToInt32(ddown010.SelectedValue));

                if (val > 0)
                {
                    lbl003.Text = "El número de resolución ingresado ya existe.";
                    lbl003.Visible = true;
                    btnGuardar_NEW.Visible = false;
                }
                else
                {
                    lbl003.Visible = false;
                    btnGuardar_NEW.Visible = true;
                }

            }
        }

        protected void txtFecInicio_TextChanged(object sender, EventArgs e)
        {
            if (txtFecInicio.Text != "" && txtFecTermino.Text != "")
            {
                if (!comparaFechas())
                {
                    lb_fi.Visible = true;
                }
                else
                {
                    lb_fi.Visible = false;
                }
            }
            if (txtFecInicio.Text != "")
            {
                //// JOVM - 19/01/2015
                ////cal004.MinDate = Convert.ToDateTime(cal003.Value).AddDays(1);
                ////cal003.BackColor = System.Drawing.Color.White;
            }
        }

        protected void txtFecTermino_TextChanged(object sender, EventArgs e)
        {
            lblMsgSuccess.Visible = false;
            alertS.Visible = false;
            alertW.Visible = false;
            lblMsgWarning.Visible = false;
            System.Drawing.Color colorCampoObligatorio = System.Drawing.ColorTranslator.FromHtml("#F2F5A9"); //gfontbrevis

            if (txtFecInicio.Text != "" && txtFecTermino.Text != "")
            {
                if (!comparaFechas())
                {
                    lb_fi.Visible = true;
                }
                else
                {
                    lb_fi.Visible = false;
                }
            }

            if (txtFecInicio.Text != "")
            {
                txtFecInicio.BackColor = System.Drawing.Color.White;
                btnGuardar_NEW.Visible = true;
            }
            else
            {
                txtFecInicio.BackColor = colorCampoObligatorio;
                btnGuardar_NEW.Visible = false;
                txtFecTermino.Text = null;
                alertW.Visible = true;
                lblMsgWarning.Visible = true;
            }
        }

        public bool comparaFechas()
        {
            string fi_text = txtFecInicio.Text;
            DateTime fi = Convert.ToDateTime(fi_text);
            string ft_text = txtFecTermino.Text;
            DateTime ft = Convert.ToDateTime(ft_text);

            if (fi > ft)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private DataTable ObtenerEstadosProyecto()
        {
            Conexiones c = new Conexiones();

            return c.EjecutaSpToDataTable("GetEstadosProyecto");
        }

        private void CargaEstadosProyecto()
        {
            var dt = ObtenerEstadosProyecto();

            rdoBtnListEstadoProyecto.DataSource = dt;
            rdoBtnListEstadoProyecto.DataTextField = "Descripcion";
            rdoBtnListEstadoProyecto.DataValueField = "CodEstadoProyecto";

            rdoBtnListEstadoProyecto.DataBind();
        }

        public enum EstadoProyecto
        {
            Adjudicado = 0,
            EnEjecucion = 1,
            Readjudicado = 2
        }

        private void AccionEstadoSeleccionadoAdjudicado()
        {
            string dd1value = ddown001.SelectedValue;
            funcion_limpiar();
            ddown001.SelectedValue = dd1value;
            if (ddown001.SelectedValue != "0")
            {
                getproyectos();
            }
        }

        private void AccionEstadoSeleccionadoEnEjecucion()
        {
            string dd1value = ddown001.SelectedValue;
            funcion_limpiar();
            ddown001.SelectedValue = dd1value;
            if (ddown001.SelectedValue != "0")
            {
                getproyectos();
            }
        }

        private void AccionEstadoSeleccionadoReadjudicado()
        {
            string dd1value = ddown001.SelectedValue;
            funcion_limpiar();
            ddown001.SelectedValue = dd1value;
            if (ddown001.SelectedValue != "0")
            {
                getproyectos();
            }
        }

        private string ObtenerEstadoProyecto(string value)
        {
            if (value == "0")
            {
                return EstadoProyecto.Adjudicado.ToString();
            }

            if (value == "1")
            {
                return EstadoProyecto.EnEjecucion.ToString();
            }

            if (value == "2")
            {
                return EstadoProyecto.Readjudicado.ToString();
            }

            return "";
        }

        protected void rdoBtnListEstadoProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoBtnListEstadoProyecto.BackColor = Color.Empty;
            var value = ObtenerEstadoProyecto(rdoBtnListEstadoProyecto.SelectedValue);

            if (value == EstadoProyecto.Adjudicado.ToString())
            {
                AccionEstadoSeleccionadoAdjudicado();
            }

            if (value == EstadoProyecto.EnEjecucion.ToString())
            {
                AccionEstadoSeleccionadoEnEjecucion();
            }

            if (value == EstadoProyecto.Readjudicado.ToString())
            {
                AccionEstadoSeleccionadoReadjudicado();
            }
        }

        public DataTable GetResolucionxIcod(int iCodResolucion)
        {
            Conexiones c = new Conexiones();

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@ICodResolucion", iCodResolucion));

            return c.EjecutaSpToDataTable("GetResolucionesXIcod", list);
        }

        public DataTable ObtenerResoluciones(int codProyecto)
        {
            Conexiones c = new Conexiones();

            List<SqlParameter> list = new List<SqlParameter>();

            list.Add(new SqlParameter("@CodProyecto", codProyecto));

            return c.EjecutaSpToDataTable("ObtenerResolucionesProyecto", list);
        }
    }
}