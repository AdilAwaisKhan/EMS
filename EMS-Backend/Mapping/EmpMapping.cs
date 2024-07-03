namespace NewApp;

public static class EmpMapping
{
    public static Employee ToEntity(this CreateDto newEmp){
        return new Employee(){
            Name = newEmp.name,
            Email = newEmp.email,
            Phno = newEmp.phno
        };
    }
    public static Employee ToEntity(this UpdateDto newEmp, int id){
        return new Employee(){
            Id = id,
            Name = newEmp.name,
            Email = newEmp.email,
            Phno = newEmp.phno
        };
    }
    public static EmpDTO ToEmpDto(this Employee employee){
        return new(
            employee.Id,
            employee.Name,
            employee.Email,
            employee.Phno
        );
    }

}
