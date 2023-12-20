using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace HospitalManagement.Pages;

public class Index_Tests : HospitalManagementWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
