﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CreaturDatos
{
    public partial class ModificarAgentes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbIdModificar.Text = Session["Id"].ToString();
                tbIdModificar.Visible = false;
                tbFactProveedor.Text = Session["FacturaProveedor"].ToString();
                tbObserAdministracion.Text = Session["ObservacionesAdministracion"].ToString();
                tbFacCreatur.Text = Session["NumFacturaCreatur"].ToString();
                tbObserIngresos.Text = Session["ObservacionIngreso"].ToString();
                cmbFacEfectivo.SelectedValue = Session["FacturaEfectiva"].ToString();
                cmbNomAgentes.SelectedValue = Session["Agente"].ToString();
                tbFechaCreacion.Text = Session["FechaCreacion"].ToString();
                cmbRefac.SelectedValue = Session["Refacturacion"].ToString();
                tbHk.Text = Session["HK"].ToString();
                tbGk.Text = Session["GK"].ToString();
                tbHuesped.Text = Session["Huesped"].ToString();
                tbEmpresa.Text = Session["Empresa"].ToString();
                tbHotel.Text = Session["Hotel"].ToString();
                tbCdHotel.Text = Session["CiudadUbicacionHotel"].ToString();
                tbConfir.Text = Session["Confirmacion"].ToString();
                tbFechaCheIn.Text = Session["FechaCheckIn"].ToString();
                tbFechaCheOut.Text = Session["FechaCheckOut"].ToString();
                tbNoche.Text = Session["NumeroNoches"].ToString();
                tbTariBasImpuesInclud.Text = Session["TarifaBaseConImpIncluidos"].ToString();
                tbTotConImpInclud.Text = Session["TotalConImpIncluidos"].ToString();
                tbTotTarifHotel.Text = Session["TotalTarifaBaseHotel"].ToString();
                cmbISH.SelectedValue = Session["ISH"].ToString();
                tbTipoMoneda.Text = Session["TipoMoneda"].ToString();
                tbPorcenGanSobTotalImpuInclud.Text = Session["PorcenGananSobreTotalImpuestosInclud"].ToString();
                tbImporteGanancia.Text = Session["ImporteGanancia"].ToString();
                cmbServiAdicionales.SelectedValue = Session["ServiciosAdicionales"].ToString();
                tbCargoServicio.Text = Session["CargoServicio"].ToString();
                tbImporteServAdicionales.Text = Session["ImporteServicioAdicional"].ToString();
                tbPorGananSerAdici.Text = Session["PorcenGananciaSobreServAdicional"].ToString();
                tbImporGanan.Text = Session["ImporteGananciaSobreServicioAdicional"].ToString();
                tbTotFactuHospedaje.Text = Session["TotalFacHospedaje"].ToString();
                tbForPagoTarje.Text = Session["FormaPago"].ToString();
                tbTipoTarifa.Text = Session["TipoTarifa"].ToString();
                tbNumTarRech.Text = Session["NumRechazadosDeTarjetas"].ToString();
                tbResEfectiva.Text = Session["ReservaEfectiva"].ToString();
                tbImpoCobrarHtl.Text = Session["ImporteCobrarHtl"].ToString();
                tbFopFinal.Text = Session["FOPFinal"].ToString();
                tbObserDirec.Text = Session["ObservacionDireccion"].ToString();
            }

            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");

            //Administracion
            tbFactProveedor.Enabled = false;
            tbObserAdministracion.Enabled = false;
            //Ingresos
            tbFacCreatur.Enabled = false;
            tbObserIngresos.Enabled = false;
            //Admin
            cmbFacEfectivo.Enabled = false;
            //Calidad
            tbNumTarRech.Enabled = false;
            //Direccion
            tbResEfectiva.Enabled = false;
            tbImpoCobrarHtl.Enabled = false;
            tbFopFinal.Enabled = false;
            tbObserDirec.Enabled = false;
        }

        protected void imgBtnHome_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("menuAgentes.aspx", true);
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            DateTime d1 = Convert.ToDateTime(tbFechaCheIn.Text);
            DateTime d2 = Convert.ToDateTime(tbFechaCheOut.Text);
            TimeSpan ds = (d2 - d1);
            tbNoche.Text = Convert.ToString(ds.Days);

            double valor2 = Convert.ToDouble(tbTariBasImpuesInclud.Text);
            double res = (Convert.ToDouble(tbNoche.Text) * valor2);
            tbTotConImpInclud.Text = Convert.ToString(res);
            double valorPorcenGanancia = Convert.ToDouble(tbPorcenGanSobTotalImpuInclud.Text);
            double resPorImporteGanancia = (res * valorPorcenGanancia) / 100;
            tbImporteGanancia.Text = Convert.ToString(resPorImporteGanancia);

            double imporServiAd = Convert.ToDouble(tbImporteServAdicionales.Text);
            double porGaSerAd = Convert.ToDouble(tbPorGananSerAdici.Text);
            double totImportPorcenSerAd = (imporServiAd * porGaSerAd) / 100;
            tbImporGanan.Text = Convert.ToString(totImportPorcenSerAd);

            double cargoServicio = Convert.ToDouble(tbCargoServicio.Text);

            double totFacHospedaje = res + resPorImporteGanancia + cargoServicio + imporServiAd + totImportPorcenSerAd;
            tbTotFactuHospedaje.Text = Convert.ToString(totFacHospedaje);






            SqlConnection con = new SqlConnection("Data Source=UNICO-PC;Initial Catalog=DBCreaturHotel; user id=creatur;password=creatur2010;MultipleActiveResultSets=True;");
            con.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE tblDatos SET FacturaProveedor ='" + tbFactProveedor.Text + "',  ObservacionesAdministracion ='" + tbObserAdministracion.Text +
            "', NumFacturaCreatur ='" + tbFacCreatur.Text + "', ObservacionIngreso ='" + tbObserIngresos.Text + "', FacturaEfectiva ='" + cmbFacEfectivo.SelectedValue +
            "', Agente ='" + cmbNomAgentes.SelectedValue + "', FechaCreacion ='" + tbFechaCreacion.Text + "', Refacturacion = '" + cmbRefac.SelectedValue +
            "', HK ='" + tbHk.Text + "', GK ='" + tbGk.Text + "', Huesped ='" + tbHuesped.Text + "', Empresa ='" + tbEmpresa.Text + "', Hotel ='" + tbHotel.Text +
            "', CiudadUbicacionHotel ='" + tbCdHotel.Text + "', Confirmacion = '" + tbConfir.Text + "', FechaCheckIn ='" + Convert.ToDateTime(tbFechaCheIn.Text) +
            "', FechaCheckOut ='" + Convert.ToDateTime(tbFechaCheOut.Text) + "', NumeroNoches = '" + Convert.ToInt32(tbNoche.Text) + "', TarifaBaseConImpIncluidos ='" + Convert.ToDouble(tbTariBasImpuesInclud.Text) +
            "', TotalConImpIncluidos ='" + Convert.ToDouble(tbTotConImpInclud.Text) + "', TotalTarifaBaseHotel ='" + Convert.ToDouble(tbTotTarifHotel.Text) +
            "', ISH ='" + cmbISH.SelectedValue + "', TipoMoneda ='" + tbTipoMoneda.Text + "', PorcenGananSobreTotalImpuestosInclud ='" + Convert.ToDouble(tbPorcenGanSobTotalImpuInclud.Text) +
            "', ImporteGanancia ='" + Convert.ToDouble(tbImporteGanancia.Text) + "', ServiciosAdicionales ='" + cmbServiAdicionales.SelectedValue +
            "', CargoServicio ='" + Convert.ToDouble(tbCargoServicio.Text) + "', ImporteServicioAdicional ='" + Convert.ToDouble(tbImporteServAdicionales.Text) +
            "', PorcenGananciaSobreServAdicional ='" + Convert.ToDouble(tbPorGananSerAdici.Text) + "', ImporteGananciaSobreServicioAdicional ='" + Convert.ToDouble(tbImporGanan.Text) +
            "', TotalFacHospedaje = '" + Convert.ToDouble(tbTotFactuHospedaje.Text) + "', FormaPago ='" + tbForPagoTarje.Text +
            "', TipoTarifa = '" + tbTipoTarifa.Text + "', NumRechazadosDeTarjetas ='" + tbNumTarRech.Text + "', ReservaEfectiva ='" + tbResEfectiva.Text +
            "', ImporteCobrarHtl ='" + tbImpoCobrarHtl.Text + "', FOPFinal ='" + tbFopFinal.Text + "', ObservacionDireccion ='" + tbObserDirec.Text + "' WHERE (Id = '" + Convert.ToInt32(tbIdModificar.Text) + "')", con);
            cmd.ExecuteNonQuery();          
            con.Close();

            Response.Write(@"<script language='javascript'>alert('Los datos se modificaron con exito');</script>");
            Response.Redirect("menuAgentes.aspx");
            
            
        }

        protected void btnGenerarBoletoAgente_Click(object sender, EventArgs e)
        {
            Session["Id"] = tbIdModificar.Text;
            Session["FechaCreacion"] = tbFechaCreacion.Text;
            Session["GK"] = tbGk.Text;
            Session["Hotel"] = tbHotel.Text;
            Session["CiudadUbicacionHotel"] = tbCdHotel.Text;
            Session["Huesped"] = tbHuesped.Text;
            Session["FechaCheckIn"] = tbFechaCheIn.Text;
            Session["FechaCheckOut"] = tbFechaCheOut.Text;
            Session["Confirmacion"] = tbConfir.Text;
            Session["NumeroNoches"] = tbNoche.Text;
            Session["FormaPago"] = tbForPagoTarje.Text;
            Session["Agente"] = cmbNomAgentes.SelectedValue;
            
           
            Response.Redirect("AdFormBoleto.aspx");
        }


    }
}