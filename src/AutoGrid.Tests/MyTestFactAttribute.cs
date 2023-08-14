using Xunit;

namespace AutoGrid.Tests
{
    public class MyTestFactAttribute :
#if NET5_0_OR_GREATER
        WpfFactAttribute
#else
        FactAttribute
#endif
    {

    }
}