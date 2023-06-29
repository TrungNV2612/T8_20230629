using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace T8_20230629
{
    internal class EmployeeManager:BaseManager
    {
        internal List<Employee> employees = new List<Employee>();               
        internal string exportPath = "E:\\0. CNTT\\0.FULL STACK\\2. Bai tap C#\\T8_20230629\\T8ExportList.csv";
        internal EmployeeManager(string name) : base(name)
        {
            Employee admin = new Employee("id0","admin","admin@gmail.com","admin",true);
            this.employees.Add(admin);            
        }
        internal int Find()
        {
            int index = -1;
            Console.WriteLine("Please enter field which to find: no, name or email ");
            string fieldFind = Console.ReadLine();
            bool isFound = false;
            foreach (Employee emp in this.employees)
            {
                index++;
                if (emp.no == fieldFind || emp.name == fieldFind || emp.email == fieldFind )
                {
                    Console.WriteLine(emp);                    
                    isFound = true;
                    break;
                }                
            }            
            if ( !isFound )
            {
                index = -1;
                Console.WriteLine(" Not found ");
            }
            return index;
        }
        internal override void AddNew()
        {            
            Employee EmpEnter = EnterEmployee();
            this.employees.Add(EmpEnter);
        }
        internal override void Update()
        {
            int index = Find();            
            if (index >= 0)
            {
                Employee empUpdate = EnterEmployee();
                this.employees[index] = empUpdate;
            }          

        }
        internal override void Delete()
        {
            int index = Find();
            if (index >= 0)
            {
                this.employees.RemoveAt(index);
            }
        }
        internal override void ViewOrder()
        {
            //List<Employee> employeesSorted = new List<Employee>();
            //employeesSorted = this.employees.OrderBy(e=>e.name).ToList();
            this.employees.Sort((x, y) => x.name.CompareTo(y.name));
            foreach (Employee emp in this.employees)
            {
                Console.WriteLine(emp);
            }
        }
        internal override void Export()
        {
            StreamWriter writer = new StreamWriter(exportPath);
            foreach (Employee emp in this.employees)
            {
                if(emp != null)
                {                    
                    writer.WriteLine(emp.ToStringAll());
                }
            }
            writer.Close();

        }
        internal override void Import()
        {
            Console.WriteLine("Please enter import path: ");
            string importPath = Console.ReadLine();
            StreamReader reader = new StreamReader(importPath);
            string line;
            do
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    string[] arrEmployee = line.Split(',');
                    string no = arrEmployee[0].Trim();
                    string name = arrEmployee[1].Trim();
                    string email = arrEmployee[2].Trim();
                    string passWord = arrEmployee[3].Trim();
                    bool isManager = Convert.ToBoolean(arrEmployee[4].Trim());
                    Employee empEnter = new Employee(no, name, email, passWord, isManager);
                    this.employees.Add(empEnter);
                }

            }
            while (line != null);
            reader.Close();
        }
        internal int CheckLogin()
        {
            Console.WriteLine(" Please Enter Your Email");
            string emailEnter = Console.ReadLine();
            Console.WriteLine(" Please Enter Your Password");
            string passwordEnter = Console.ReadLine();
            int check = 0;
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].email.Equals(emailEnter) && (employees[i].password.Equals(passwordEnter)) && (employees[i].isManager == true))
                {
                    check = 1;
                    break;
                }
                else if (employees[i].email.Equals(emailEnter) && (employees[i].password.Equals(passwordEnter)) && (employees[i].isManager == false))
                {
                    check = 2;
                    break;
                }
                else check = 3;

            }
            return check;
        }
        internal Employee EnterEmployee() 
        {            
            Employee empEnter = null;
            bool isStop = false;
            while (! isStop)
            {
                Console.WriteLine(" Fill information Employee according to format: no,name,email,passWord,isManager or exit");
                String empInfor = Console.ReadLine();
                if( empInfor == "exit" )
                {
                    isStop = true;
                }
                else
                {
                    int numberChar = 0;
                    foreach (char ch in empInfor)
                    {
                        if (ch == ',')
                        {
                            numberChar++;
                        }
                    }
                    if (numberChar == 4)
                    {
                        string[] empInforArray = empInfor.Split(',');
                        string no = empInforArray[0].Trim();
                        string name = empInforArray[1].Trim();
                        string email = empInforArray[2].Trim();
                        string passWord = empInforArray[3].Trim();
                        bool isManager = false;
                        try
                        {
                            isManager = Convert.ToBoolean(empInforArray[4].Trim());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(" Error: " + ex.Message);
                            continue;
                        }

                        isStop = true;
                        empEnter = new Employee(no, name, email, passWord, isManager);

                    }
                    else
                    {
                        Console.WriteLine(" Please enter enough field ");
                    }
                }
                
            }
            return empEnter;            
        }
    }
}
