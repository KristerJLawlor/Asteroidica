half Overlay(half a, half b)
{
    half r = b < .5 ? 2.0 * b * a : 1.0 - 2.0 * (1.0 - b) * (1.0 - a);
    return r;
}

half3 Overlay(half3 a, half3 b)
{
    half3 r = b < .5 ? 2.0 * b * a : 1.0 - 2.0 * (1.0 - b) * (1.0 - a);
    return r;
}

half4 Overlay(half4 a, half4 b)
{
    half4 r = b < .5 ? 2.0 * b * a : 1.0 - 2.0 * (1.0 - b) * (1.0 - a);
    r.a = b.a;
    return r;
}