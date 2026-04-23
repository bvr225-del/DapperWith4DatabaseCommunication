namespace DapperWith4DatabaseCommunication.Utils
{
    public static class StoredprocedureParameters
    {
        #region Department Parameters
        public static string DeptId = "deptid";
        public static string DeptName = "deptname";
        public static string DeptLocation = "deptlocation";
        public static string DeptinsertedVariable = "insertedvalue";
        #endregion

        #region Order Parameters
        public static string OrderId = "orderid";  //@orderid
        public static string OrderName = "ordername";
        public static string OrderLocation = "orderlocation";
        #endregion

        #region Employee Parameters
        public static string EmployeeID = "empid";
        public static string EmployeeName = "empname";
        public static string EmployeeSalary = "empsalary";
        public static string Insertedvariable = "insertvalue";
        #endregion

        #region Restaurant Parameters
        public static string ID = "Id";
        public static string RestaurantName = "RestaurantName";
        public static string RestaurantLocation = "RestaurantLocation";
        public static string CreationDate = "CreationDate";
        #endregion

    }
}
