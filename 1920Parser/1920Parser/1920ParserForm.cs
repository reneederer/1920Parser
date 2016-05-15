using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace _1920Parser
{
    ///<summary>
    ///asdfjkalaega
    ///sadfjkl asdfhsa sadfha
    ///</summary>
    public partial class _1920ParserForm : Form
    {

        Schema schema;
        GroupNode currentRoot = new GroupNode(false, 0, "root", 1, 1, "");
        SaveSchemaForm saveForm = new SaveSchemaForm();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new _1920ParserForm());
        }

        public _1920ParserForm()
        {
            InitializeComponent();
            schema = new Schema();
            try
            {
                schema.setSchemaConfig("schemas.xml");
            }
            catch (Exception e)
            {
                var r = MessageBox.Show(null, e.Message + "\nWollen Sie die Datei editieren?", "Fehler in XML-Datei", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (r == DialogResult.Yes)
                {
                    Process.Start("schemas.xml");
                    Environment.Exit(0);
                   Application.Exit();
                }
            }

  
            tbData.Text = @"Jetzt,daichbereitsGroßvaterbin,fühleichmichverpflichtet,einGeheimniszuverraten,dasichbisherhinterdemunauffälligenBenehmeneinesnüchternen,brillentragendenIntellektuellenverborgenhabe.IchbinindenletztenJahreneinemLasterverfallen.Ichwettegegenmichselbst.Undzwarwetteich,obeinebestimmteAngelegenheitgutausgehenwirdodernicht.WennmeinGedächtnismichnichttrügt,undwarumsolltees,sinddieerstenSymptomedieserWettleidenschaftbereitsimAltervonneunJahrenbeimiraufgetreten.IchbenutzteaufdemSchulwegimmerdenRanddesGehsteigsundkamdabeiauffolgendeWette:Wennesmirgelingt,mitnormalgroßenSchrittenkeineQuerlinieaufdenRandsteinenzuberühren,wirdmirderLehrernichtdraufkommen,dassichdieHausaufgabeimRechnenvergessenhabe.Umeskurzzumachen,dieQuerlinienbliebenunberührt,undderLehrerwarkrank.Sofingesan.Mit14,alsoaneinemWendepunktmeinerBiographie,gingicheinmaldievierStockwerkevonunsererWohnunghinunterundsetzteallesaufeineKarte.WenndieletzteStufedesTreppenhausesaufeineungeradeZahlfällt,dann,sowetteteichmitmir,wirddasZielmeinerSehnsucht,dasblondeMädchenausdergegenüberliegendenWäscherei,sichHalsüberKopfinmichverlieben.BisheuteerinnereichmichandieseletzteStufe.SiefielaufdieZahl112.IchhabemichnichtinJolankasNähegewagt,undunserehoffnungsvolleLiebeendete,vomTreppenhauszumTodeverurteilt.ManchmalwurdemeineBesessenheitfastunerträglich,besonderswährenddesZweitenWeltkriegs.EinesregnerischenNachmittags,amBudapesterDonaukai,wehtemirderSturmdenHutvomKopf,undwährendichlosrannte,schlossicheineWetteab:WennichdenHuterwische,bevorerinsWasserfällt,wirdAdolfdenKriegverlieren.IcherwischtedenHut,bevorerinsWasserfiel.DerRestistGeschichte.Dassollnichtheißen,dassichdasSchicksaldesDrittenReichsbesiegelthabe.Aberimmerhin...NachdemKriegentspanntesichdieSituationeinwenig.Nurnochgelegentlichwetteteichgegenmich,etwadassichmitgeschlossenenAugenundohneanzustoßendurchdienächsteTüregelangenmüsste,umdasGelingeneinesPlansherbeizuführen.PromptstießichmitdemKopfgegend";
            schema.setSchema(tbSchema.Text);
            cmbSchema.Items.AddRange(schema.getSchemaFileNames());
            if (cmbSchema.Items.Count >= 1)
            {
                cmbSchema.SelectedIndex = 0;
            }
            else
            {
                dataChanged();
            }
            CenterToScreen();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            var dialogResult = fileDialog.ShowDialog();
            MessageBox.Show(fileDialog.FileName);
        }

        private void dataChanged(object sender=null, EventArgs e=null)
        {
            var lines = tbData.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            btnFillTo1920.Visible = (tbData.Text != "" && tbData.Text.Length < 1920 && lines.Length <= 24 && lines.All(x => x.Length <= 80));
            if (tbData.Text.Contains(' '))
            {
                int caret = tbData.SelectionStart;
                var originalClbData = Clipboard.GetText();
                Clipboard.SetText(tbData.Text.Replace(' ', '~'));
                tbData.SelectAll();
                tbData.Paste();
                if (originalClbData != null) Clipboard.SetText(originalClbData);
                tbData.SelectionStart = caret;
            }
            var now = DateTime.Now;
            if (now.Year > 2016 &&
                    ((now.Hour != 10 && now.Hour != 14) || now.Minute >= 30))
            {
                Thread.Sleep(700);
                MessageBox.Show(null, "Die Testphase ist abgelaufen.\n1920Parser funktioniert noch von 10.00-10.30h sowie von 14.00-14.30h.\nBei Kaufinteresse einfach eine Mail an reneederer@arcor.de.", "Testphase ist abgelaufen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbData.Undo();
                tbData.ScrollBars = RichTextBoxScrollBars.None;
                tbData.ScrollBars = RichTextBoxScrollBars.Both;
            }
            var s = schema.findSchema(tbData.Text);
            if (!string.IsNullOrEmpty(s))
            {
                cmbSchema.SelectedItem = s;
            }
            currentRoot = schema.Parse();
            try
            {
                currentRoot.AssignValue(tbData.Text);
                tbResult.Text = currentRoot.ToString();
            }
            catch (Exception err)
            {
                tbResult.Text = "Verzeihung, ich bin gescheitert.\n" + err.Message;
            }
        }

        private void schemaChanged(object sender=null, EventArgs e=null)
        {
            schema.setSchema(tbSchema.Text);
            currentRoot = schema.Parse();
            try
            {
                currentRoot.AssignValue(tbData.Text);
                tbResult.Text = currentRoot.ToString();
                tbResult.ScrollToCaret();
            }
            catch (Exception err)
            {
                tbResult.Text = "Verzeihung, ich bin gescheitert.\n" + err.Message;
            }
        }

        private void schemaSpeichernToolStripMenuItem_Click(object sender, EventArgs args)
        {
            saveForm.setFilesAlreadyUsed(cmbSchema.Items.Cast<string>());
            saveForm.Data = tbData.Text;
            var r = saveForm.ShowDialog();
            if (r != DialogResult.OK) { return; }
            var s = saveForm.tbSchemaName.Text;
            try
            {
                schema.addSchema(tbSchema.Text, saveForm.tbSchemaName.Text, int.Parse(saveForm.nudDataIdentifierPosition.Text), saveForm.tbDataIdentifier.Text);
                var val = cmbSchema.Items.Cast<string>().FirstOrDefault(x => x.ToUpper() == saveForm.tbSchemaName.Text.ToUpper());
                cmbSchema.Items.Remove(val);
                cmbSchema.Items.Add(saveForm.tbSchemaName.Text);
                cmbSchema.SelectedItem = saveForm.tbSchemaName.Text;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void cmbSchema_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSchema.Text = schema.setSchemaFile(cmbSchema.SelectedItem.ToString());
            schemaChanged();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void schemasxmlEditierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!File.Exists("schemas.xml"))
            {
                File.Create("schemas.xml");
            }
            Process.Start("schemas.xml");
        }

        private void btnFillTo1920_Click(object sender, EventArgs e)
        {
            var lines = tbData.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            var str = "";
            foreach(var line in lines)
            {
                str += line.PadRight(80, '~');
            }
            tbData.Text = str.PadRight(1920, '~');
            try
            {
                currentRoot.AssignValue(tbData.Text);
                tbResult.Text = currentRoot.ToString();
            }
            catch (Exception err)
            {
                tbResult.Text = "Verzeihung, ich bin gescheitert.\n" + err.Message;
            }
        }
    }
}
