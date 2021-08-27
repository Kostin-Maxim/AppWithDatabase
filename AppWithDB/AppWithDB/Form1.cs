using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppWithDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void peopleBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.peopleBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.People". При необходимости она может быть перемещена или удалена.
            this.peopleTableAdapter.Fill(this.database1DataSet.People);

        }

        
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            //Активация кнопки для сохранения ввода
            button1.Enabled = true;

            //Блокирование верхней панели
            peopleBindingNavigator.Enabled = false;

            // Генерация индификатора
            Guid guid = Guid.NewGuid();
            idTextBox.Text = guid.ToString();

            
        }


        // Проверка ФИО
        private bool ValidationFIO(string surname, string name, string middleName)
        {
            int parsedValue;
         
            if (int.TryParse(surname, out parsedValue) || int.TryParse(name, out parsedValue) || int.TryParse(middleName, out parsedValue))
            {
                MessageBox.Show("Некоректно введенные ФИО!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        // Проверка электронной почты
        private bool ValidationEmail(string email)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(email);
        }

        // Проверка телефона
        private bool ValidationPhone(string phone)
        {
            int parsedValue;
            if (!int.TryParse(phone, out parsedValue))
            {
                MessageBox.Show("Некоректно введен номер телефона!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool checkEmail = false;
            bool checkFIO = ValidationFIO(surnameTextBox.Text, nameTextBox.Text, middle_NameTextBox.Text);
            if (!ValidationEmail(emailTextBox.Text))
            {
                MessageBox.Show("Некоректно введена электронная почта!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkEmail = true;
            }
            bool checkPhone = ValidationPhone(phoneTextBox.Text);

            if (!checkEmail && !checkFIO && !checkPhone)
            {
                button1.Enabled = false;
                peopleBindingNavigator.Enabled = true;
            }
        }
    }
}
