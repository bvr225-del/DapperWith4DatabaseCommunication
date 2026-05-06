namespace DapperWith4DatabaseCommunication.Utils
{
    public static class StoredprocedureParameters
    {
        #region Department Parameters
        public static string DeptId = "@deptid";
        public static string DeptName = "@deptname";
        public static string DeptLocation = "@deptlocation";
        public static string DeptinsertedVariable = "@insertedvalue";
        #endregion

        #region Order Parameters
        public static string OrderId = "@orderid";
        public static string OrderName = "@ordername";
        public static string OrderLocation = "@orderlocation";
        public static string OrderInsertedvariable = "@insertedvalue";

        #endregion

        #region Employee Parameters
        public static string EmployeeID = "@empid";
        public static string EmployeeName = "@empname";
        public static string EmployeeSalary = "@empsalary";
        public static string Insertedvariable = "@insertvalue";
        #endregion

        #region Restaurant Parameters
        public static string ID = "@id";
        public static string RestaurantName = "@restaurantname";
        public static string RestaurantLocation = "@restaurantlocation";
        public static string CreationDate = "@creationdate";
        public static string RestaurantInsertedvariable = "@insertvalue";

        #endregion

        #region Logging Parameters

        public static string Logging_UserName = "@username";
        public static string Logging_LogLevel = "@LogLevel";
        public static string Logging_MessageTemplate = "@MessageTemplate";
        #endregion


    }
}
