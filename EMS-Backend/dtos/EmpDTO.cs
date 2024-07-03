using System.ComponentModel.DataAnnotations;

namespace NewApp;

public record class EmpDTO(int id, string name, string email, long phno);