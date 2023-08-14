using Xunit;
using Xunit.Extensions;

namespace AutoGrid.Tests
{
    public class MyTestTheoryAttribute :
#if NET5_0_OR_GREATER
        WpfTheoryAttribute
#else
        TheoryAttribute
#endif
    {

    }
}