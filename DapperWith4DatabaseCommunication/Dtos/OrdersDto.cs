namespace DapperWith4DatabaseCommunication.Dtos
{
    public class OrdersDto
    {
        public int orderid { get; set; }

        public string ordername { get; set; }

        public string orderlocation { get; set; }
        ////it is used for condition checking purpose in service layer and it is not a part of database table so we are not adding this property in orders model class which is used for database communication purpose.
        public string Flag { get; set; }

    }
}
