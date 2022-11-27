namespace DO;

public struct Orders
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public override string ToString() => $@"
CustomerOrder ID={ID}: {CustomerName}, 
Email : {CustomerEmail}
    	Adress: {CustomerAdress}
    	OrderDate: {OrderDate}
ShipDate: {ShipDate}
DeliveryDate: {DeliveryDate}
";

}
