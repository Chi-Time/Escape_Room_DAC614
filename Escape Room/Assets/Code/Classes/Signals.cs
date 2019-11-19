using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Signals
{
    public static event Action<Collectable> OnCollected;
    public static void Collect (Collectable collectable) { OnCollected?.Invoke (collectable); }

    public static event Action<Inspectable> OnInspected;
    public static void Inspect (Inspectable inspectable) { OnInspected?.Invoke (inspectable); }
}
