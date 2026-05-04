using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace location_operations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        EfTravelDbEntities db = new EfTravelDbEntities();
        private void btnList_Click(object sender, EventArgs e)
        {
            var values = db.Location.ToList();
            dataGridView1.DataSource = values;
            dataGridView1.Columns["Guide"].Visible = false;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Location location = new Location();

            if (txtCity.Text == "")
            {
                MessageBox.Show("Şehir boş bırakılamaz!");
                return;
            }

            if (txtCountry.Text == "")
            {
                MessageBox.Show("Ülke boş bırakılamaz!");
                return;
            }

            if (nudCapasity.Value <= 0)
            {
                MessageBox.Show("Kapasite 0'dan büyük olmalıdır!");
                return;
            }

            if (txtDayNight.Text == "")
            {
                MessageBox.Show("Gün Gece boş bırakılamaz!");
                return;
            }

            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Lütfen geçerli bir fiyat giriniz!");
                return;
            }
            
            location.City = txtCity.Text;
            location.Country = txtCountry.Text;
            location.Capacity = Convert.ToByte(nudCapasity.Value);
            location.Price = price;
            location.DayNight = txtDayNight.Text;
            location.GuideId = int.Parse(cmbGuide.SelectedValue.ToString());

            db.Location.Add(location);
            db.SaveChanges();

            MessageBox.Show("Ekleme işlemi başarılı!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var values = db.Guide.Select(x => new
            {
                FullName = x.GuideName + " " + x.GuideSurname,
                x.GuideId
            }).ToList();
            cmbGuide.DisplayMember = "FullName";
            cmbGuide.ValueMember = "GuideId";
            cmbGuide.DataSource = values;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("Lütfen geçerli bir ID giriniz!");
                return;
            }

            var deletedValue = db.Location.Find(id);

            if (deletedValue == null)
            {
                MessageBox.Show("Böyle bir kayıt bulunamadı!");
                return;
            }

            db.Location.Remove(deletedValue);
            db.SaveChanges();
            MessageBox.Show("Silme işlemi başarılı!");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("Lütfen geçerli bir ID giriniz!");
                return;
            }

            var values = db.Location.Find(id);

            if (values == null)
            {
                MessageBox.Show("Böyle bir kayıt bulunamadı!");
                return;
            }

            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Lütfen geçerli bir fiyat giriniz!");
                return;
            }

            values.City = txtCity.Text;
            values.Country = txtCountry.Text;
            values.Capacity = Convert.ToByte(nudCapasity.Value);
            values.Price = price;
            values.DayNight = txtDayNight.Text;
            values.GuideId = int.Parse(cmbGuide.SelectedValue.ToString());

            db.SaveChanges();
            MessageBox.Show("Güncelleme işlemi başarılı!");



        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("Lütfen geçerli bir ID giriniz!");
                return;
            }
            var values = db.Location.Find(id);
            if (values == null)
            {
                MessageBox.Show("Böyle bir kayıt bulunamadı!");
                return;
            }
           var getByIdValues = db.Location.Where(x => x.LocationId == id).ToList();
            dataGridView1.DataSource = getByIdValues;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
