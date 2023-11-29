﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab._4
{
    public partial class Form1 : Form
    {
        private List<double> numbers = new List<double>();

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // w/o do
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // w/o do
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Вы действительно хотите закрыть программу?",
                "Выход",
                MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inputData inputDataDialog = new inputData(this);
            inputDataDialog.ShowDialog();
        }

        public void UpdateNumbers(List<double> newNumbers)
        {
            numbers = newNumbers;
        }

        public void UpdateNumbers(List<int> newNumbers)
        {
            List<double> convertedNumbers = newNumbers.Select(number => (double)number).ToList();
            numbers = convertedNumbers;
        }

        private List<double> GetNumbersFromInputData()
        {
            return numbers;
        }

        private List<double> RemoveDuplicates(List<double> numbers)
        {
            return numbers.Distinct().ToList();
        }

        private void DisplayNumbersInDataGridView(List<double> numbers, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            for (int i = 0; i < numbers.Count; i++)
            {
                dataGridView.Columns.Add("Column" + i, "A[" + i + "]");
            }
            DataGridViewRow row = new DataGridViewRow();
            foreach (var number in numbers)
            {
                DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                cell.Value = number;
                row.Cells.Add(cell);
            }
            dataGridView.Rows.Add(row);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<double> numbers = GetNumbersFromInputData();
            DisplayNumbersInDataGridView(numbers, dataGridView1);
            List<double> copy2 = new List<double>(numbers);
            List<double> copy3 = new List<double>(numbers);
            HashSet<double> uniqueNumbersSet = new HashSet<double>();
            List<double> uniqueNumbers = new List<double>();
            foreach (var number in numbers)
            {
                if (!uniqueNumbersSet.Contains(number))
                {
                    uniqueNumbersSet.Add(number);
                    uniqueNumbers.Add(number);
                }
                else
                {
                    uniqueNumbers.RemoveAll(num => num == number);
                }
            }
            DisplayNumbersInDataGridView(uniqueNumbers, dataGridView2);
            if (copy2.Count > 0)
            {
                double firstNumber = copy2[0];
                copy2.RemoveAll(num => num == firstNumber);
                copy2.Insert(0, firstNumber);
            }
            DisplayNumbersInDataGridView(copy2, dataGridView3);
            double average = numbers.Any() ? numbers.Average() : 0;
            copy3.RemoveAll(num => num == average);
            DisplayNumbersInDataGridView(copy3, dataGridView4);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            properties propertiesDialog = new properties();
            propertiesDialog.ShowDialog();
        }

        public string GetDataGridViewText(DataGridView dataGridView)
        {
            StringBuilder text = new StringBuilder();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    text.Append(cell.Value + "\t");
                }
                text.AppendLine();
            }
            return text.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string textToSave = "Исходный массив:\n"
                                + GetDataGridViewText(dataGridView1)
                                + "\nМассив после удаления равных между собой элементов:\n"
                                + GetDataGridViewText(dataGridView2)
                                + "\nМассив после удаления равных первому элементу:\n"
                                + GetDataGridViewText(dataGridView3)
                                + "\nМассив после удаления равных среднему арифметическому:\n"
                                + GetDataGridViewText(dataGridView4);
            saving savingForm = new saving();
            savingForm.SetGeneratedText(textToSave);
            savingForm.ShowDialog();
        }
    }
}
