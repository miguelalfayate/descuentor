namespace Descuentor.WebAssembly.Modelos.Responses;

public class ApiErrorResponse
{
    
    // TODO: Borrar esta clase si no se utiliza
    // public string Type { get; set; }
    // public string Title { get; set; }
    // public int Status { get; set; }
    public bool Succeeded { get; set; }
    //public Dictionary<string, List<string>> Errors { get; set; }
    public List<ApiErrorDetailsResponse> Errors { get; set; }
}