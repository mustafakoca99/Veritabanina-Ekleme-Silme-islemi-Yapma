using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace adonet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //bağlantı için kullanılan kodlar
        SqlConnection connection = new SqlConnection("Server=.;Database=ekle; trusted_connection=true");
        SqlCommand eklemek = new SqlCommand();
        DataSet dataset = new DataSet();
        SqlCommand sil = new SqlCommand();
        SqlCommand guncelle = new SqlCommand();

        private void eklebuton_Click(object sender, EventArgs e)
        {
            //bağlantı açtık
            connection.Open();
            try
            {
                //eklemek için kullanılacak komutlar
                //parametre kullanarak ekleme işlemi yaptım
                eklemek = connection.CreateCommand();
                eklemek.CommandText = "Insert Into ekleisimsoyadbolum (ad,soyad,bolum) VALUES (@Ad,@Soyad,@Bolum)";
                _ = eklemek.Parameters.Add("@Ad", ad.Text);
                _ = eklemek.Parameters.Add("@Soyad", soyad.Text);
                eklemek.Parameters.Add("@Bolum", bolumtext.Text);
                //hata uyarı kodları
                if (eklemek.ExecuteNonQuery()==1)
                {
                    MessageBox.Show("Ekleme işlemi başarılı", "Başarılı sonuç...");
                }
            }
                //eklenmediği zaman uyarı verecek
            catch (Exception ex)
            {
                MessageBox.Show("Eklenemedi");
            }
            //bağlantı kapıyor
            connection.Close();
        }

        private void listelebtn_Click(object sender, EventArgs e)
        {
            //veri varsa temizlemek için
            dataset.Clear();
            connection.Open();
            //veritabanında bulunan verileri string ifadeleri göstermek için kullanır
            string goster = "select*from ekleisimsoyadbolum";
            //göster ile bağlantıyı birleştirir
            SqlDataAdapter adapter = new SqlDataAdapter(goster,connection);
            //dataseti veritabanı tablosuna ekler
            adapter.Fill(dataset, "ekleisimsoyadbolum");
            dataGridView1.DataSource = dataset;
            //datagridviewde göstermek için
            dataGridView1.DataMember = "ekleisimsoyadbolum";
            //programda ki başlıkları düzelticek
            dataGridView1.Columns["ad"].HeaderText = "Adı";
            dataGridView1.Columns["soyad"].HeaderText = "Soyadı";
            dataGridView1.Columns["bolum"].HeaderText = "Alanı";
            //datagridview şekillendirme
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("comic sans", 8, FontStyle.Bold); 
            connection.Close();
            dataset.Dispose();
            adapter.Dispose();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            connection.Open();
            //silmek için kullanılan parametrik kodlar
            sil = connection.CreateCommand();
            sil.CommandText = "delete from ekleisimsoyadbolum where Ad=@Ad";
            sil.Parameters.Add("@Ad", ad.Text);
            //hata verdirme kodu
            if (sil.ExecuteNonQuery()==1)
            {
                MessageBox.Show("Kayıt Silindi");
            }
            else
	        {
                    MessageBox.Show("Kayıt silme gerçekleşmedi");
	        }
            connection.Close();
        }
            //datagridview bilgileri üstüne tıklayınca textbox'ları doldurma
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedValue = dataGridView1.CurrentRow.Index;
            ad.Text = dataGridView1.Rows[selectedValue].Cells["ad"].Value.ToString();
            soyad.Text = dataGridView1.Rows[selectedValue].Cells["soyad"].Value.ToString();
            bolumtext.Text = dataGridView1.Rows[selectedValue].Cells["bolum"].Value.ToString();
        }
        //combobox'ı doldurma
        private void comboBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
            dataset.Clear();
            connection.Open();
            string sorgu = "select bolum from ekleisimsoyadbolum";
            SqlDataAdapter mustafa = new SqlDataAdapter(sorgu, connection);
            mustafa.Fill(dataset, "ekleisimsoyadbolum");
            foreach (DataRow koca in dataset.Tables["ekleisimsoyadbolum"].Rows)
            {
                string deger = koca[0].ToString();
                bool durum = comboBox1.Items.Contains(deger);
                if (!durum)
                {
                    comboBox1.Items.Add(deger).ToString();
                }
                connection.Close();
        }
        }

        private void Btnguncelle_Click(object sender, EventArgs e)
        {
            connection.Open();
            //güncellemek için kullanılan parametrik kodlar
            guncelle = connection.CreateCommand();
            guncelle.CommandText = "UPDATE ekleisimsoyadbolum SET ad=@Ad,soyad=@Soyad,bolum=@Bolum where=id";
            guncelle.Parameters.Add("@Ad", ad.Text);
            guncelle.Parameters.Add("@Soyad", ad.Text);
            guncelle.Parameters.Add("@Bolum", ad.Text);
            //hata verdirme kodu
            if (guncelle.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Kayıt Silindi");
            }
            else
            {
                MessageBox.Show("Kayıt silme gerçekleşmedi");
            }
            connection.Close();
        }
    }
    }
}
