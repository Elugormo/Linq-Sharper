using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using lab2;
using System;

namespace lab2_tests
{
    [TestClass]
    public class UnitTest1
    {
        List<Project> projectList = new List<Project>
            {
                new Project(1, "asd"),
                new Project(2, "bsd"),
            };


        [TestMethod]
        public void Test1()
        {


            projectList.Add(new Project { EmployeeId = 3, Name = "Asd" });

            Assert.IsTrue(projectList.Count == 3);
        }

        [TestMethod]
        public void TestWhere()
        {
            projectList.Add(new Project { EmployeeId = 3, Name = "Asd" });

            // 1, 2 task

            var newProjectList = from project in projectList where project.EmployeeId % 2 == 0 select project;

            Assert.IsTrue(newProjectList.ToList().Count == 1);
        }

        [TestMethod]
        public void TestSelect()
        {
            projectList.Add(new Project { EmployeeId = 3, Name = "Asd" });

            // 1, 2 task

            var newProjectList = from project in projectList select project.EmployeeId;

            Assert.IsTrue(newProjectList.ToList()[0] == 1);
        }

        [TestMethod]
        public void TestDictionary()
        {
            Dictionary<int, Employee> projectMapping = new Dictionary<int, Employee>()
            {
                { 2, new Employee{ Name = "Domi", ProjectId = 1, Salary = 100 } },
                { 4, new Employee{ Name = "Kate", ProjectId = 1, Salary = 123123 } }
            };
            projectMapping.Remove(2);

            Assert.IsTrue(projectMapping[4].Name == "Kate");
        }

        [TestMethod]
        public void TestExtensionMethods()
        {
            List<Employee> employeeList = new List<Employee>
            {
                new Employee("John", 1000, 1),
                new Employee("Joe", 1234, 2),
                new Employee("Julia", 349, 2),
                new Employee("Nicole", 345, 3),
            };

            var projectListIds = from project in projectList select new { project.EmployeeId };

            Dictionary<int, Employee> projectMapping = new Dictionary<int, Employee>();

            foreach (var id in projectListIds)
            {
                projectMapping.Add(id.EmployeeId, employeeList[id.EmployeeId]);
            }

            Dictionary<int, Employee> projectMappingToUnite = new Dictionary<int, Employee>()
            {
                { 2, new Employee{ Name = "Domi", ProjectId = 1, Salary = 100 } },
                { 4, new Employee{ Name = "Kate", ProjectId = 1, Salary = 123123 } }
            };

            projectMappingToUnite.AddRangeNewOnly(projectMapping);


            Assert.IsTrue(projectMapping.Count == 2);
            Assert.IsTrue(projectMappingToUnite.Count == 3);


        }

        [TestMethod]
        public void TestAnonymousClasses()
        {
            var selectedList = from project in projectList
                               where project.Name[0] != 'b'
                               select new { project.Name, project.EmployeeId };
            
            Assert.IsTrue(selectedList.ToList().Count == 1); 
        }

        [TestMethod]
        public void TestIComparer()
        {

            List<Employee> employeeList = new List<Employee>
            {
                new Employee("John", 1000, 1),
                new Employee("Joe", 1234, 2),
                new Employee("Julia", 349, 2),
                new Employee("Nicole", 345, 3),
            };
            var listToArray = employeeList.Select(a => a.Salary).ToArray();
            Array.Sort(listToArray, 0, listToArray.Length, new ReverseComparer());

            Assert.IsTrue(isSorted(listToArray));
        }

        [TestMethod] 
        public void TestConvertListToArray()
        {
            List<Employee> employeeList = new List<Employee>
            {
                new Employee("John", 1000, 1),
                new Employee("Joe", 1234, 2),
                new Employee("Julia", 349, 2),
                new Employee("Nicole", 345, 3),
            };

            Assert.IsInstanceOfType(employeeList.ToArray(), typeof(Array)) ; 
        }

        [TestMethod]
        public void TestSortingArrayByName()
        {
            List<Employee> employeeList = new List<Employee>
            {
                new Employee("John", 1000, 1),
                new Employee("Kyle", 1234, 2),
                new Employee("Mike", 349, 2),
                new Employee("Nicole", 345, 3),
            };

            var sortedList = from emp in employeeList
                             where emp.Salary > 400
                             orderby emp.Name
                             select emp;

            Assert.IsTrue(isSortedString(sortedList.ToArray()));
        }

        static bool isSortedString(Employee[] a)
        {
            int i = a.Length - 1; if (i < 1) return true;
            char ai = a[i--].Name[0]; while (i >= 0 && ai >= (ai = a[i].Name[0])) i--;
            return i < 0;
        }

        static bool isSorted(int[] a)
        {
            int i = a.Length - 1; if (i < 1) return true;
            int ai = a[i--]; while (i >= 0 && ai >= (ai = a[i])) i--;
            return i > 0;
        }
    }
}
