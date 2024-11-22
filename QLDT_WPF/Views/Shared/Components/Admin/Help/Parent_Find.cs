using QLDT_WPF.Views.Components;
using QLDT_WPF.Views.Shared.Components.Admin.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QLDT_WPF.Views.Shared.Components.Admin.Help
{
    internal class Parent_Find
    {

        public Parent_Find() { }
        public static Object Get_Template(string name, string parent,string id)
        {
            if (name == "LopHocPhanTableView")
            {
                var lopHocPhanTableView = new LopHocPhanTableView();
                return lopHocPhanTableView;
            }
            if (name == "SinhVienTableView")
            {
                var sinhVienDetails = new SinhVienTableView();
                return sinhVienDetails;
            }
            if (name == "TeacherTableView")
            {
                var teacherDetails = new TeacherTableView();
                return teacherDetails;
            }
            if (name == "KhoaDetails")
            {
                var khoaDetails = new KhoaDetails(id, parent);
                return khoaDetails;
            }
            if (name == "ChuongTrinhHocDetails")
            {
                var chuongTrinhHocDetails = new ChuongTrinhHocDetails(id, parent);
                return chuongTrinhHocDetails;
            }
            if (name == "ChuongTrinhHocTableView")
            {
                var monHocDetails = new ChuongTrinhHocTableView();
                return monHocDetails;
            }
            if(name == "SinhVienDetails")
            {
                var sinhVienDetails = new SinhVienDetails(id, parent);
                return sinhVienDetails;
            }
            if(name == "DepartmentTableView")
            {
                var departmentTableView = new DepartmentTableView();
                return departmentTableView;
            }
            if (name =="LophocphanDetails")
            {
                var lophocphanDetails = new LopHocPhanDetails(id);
                return lophocphanDetails;
            }
            if(name == "SubjectDetails")
            {
                var subjectDetails = new SubjectDetails(id, parent);
                return subjectDetails;
            }
            if(name == "SubjectsTableView")
            {
                var subjectsTableView = new SubjectsTableView();
                return subjectsTableView;
            }
            if(name == "TeacherDetails")
            {
                var teacherDetails = new TeacherDetails(id, parent);
                return teacherDetails;
            }
            return null;





        }
    }

    
}
