namespace DapperWith4DatabaseCommunication.Utils
{
    public static class StoredprocedureNames
    {
        #region Department stored procedures
        public static string AddDepartment = "Usp_AddDepartment";
        public static string UpdateDepartment = "Usp_UpdateDepartment";
        public static string DeleteDepartment = "Usp_DeleteDepartment";
        public static string GetDepartment = "Usp_GetDepartment";
        public static string GetDepartmentByDeptId = "Usp_GetDepartmentById";
        #endregion

        #region order stored procedures
        public static string AddOrder = "Usp_AddOrder";
        public static string UpdateOrder = "Usp_UpdateOrder";
        public static string DeleteOrder = "Usp_DeleteOrder";
        public static string GetOrder = "Usp_GetOrder";
        public static string GetOrderByOrderId = "Usp_GetOrderById";
        #endregion

        #region Employee storedprocedures
        public static string AddEmployee = "Usp_AddEmployeeReturn";
        public static string UpdateEmployee = "Usp_UpdateEmployee";
        public static string DeleteEmployee = "Usp_DeleteEmployee";
        public static string GetEmployee = "Usp_GetEmployee";
        public static string GetEmployeeByEmpid = "Usp_GetEmployeeId";
        #endregion

        #region restaurant storedprocedures
        public static string AddRestaurant = "Usp_AddRestaurant";
        public static string UpdateRestaurant = "Usp_UpdateRestaurant";
        public static string DeleteRestaurant = "Usp_DeleteRestaurant";
        public static string GetRestaurant = "Usp_GetRestaurant";
        public static string GetRestaurantById = "Usp_GetRestaurantById";
        #endregion

    }
}
