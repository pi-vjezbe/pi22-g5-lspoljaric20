using Evaluation_Manager.Models;
using Evaluation_Manager.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evaluation_Manager
{
    public partial class FrmStudentReportView : Form
    {
        public FrmStudentReportView()
        {
            InitializeComponent();
        }

        private void FrmStudentReportView_Load(object sender, EventArgs e)
        {
            var results = GenerateStudentReport();
            dgvResults.DataSource = results;
        }

        private List<StudentReportView> GenerateStudentReport()
        {
            var allStudents = StudentRepository.GetStudents();
            var examReports = new List<StudentReportView>();
            foreach (var student in allStudents)
            {
                if(student.HasSignature() == true)
                {
                    var examReport = new StudentReportView(student);
                    examReports.Add(examReport);
                }
            }
            return examReports;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
