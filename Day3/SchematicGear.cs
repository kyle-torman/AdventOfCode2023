public record SchematicGear
{
    private readonly SchematicNumber _partNumber1;
    private readonly SchematicNumber _partNumber2;

    public SchematicGear(SchematicNumber partNumber1, SchematicNumber partNumber2)
    {
        _partNumber1 = partNumber1;
        _partNumber2 = partNumber2;
    }

    public int GearRatio => _partNumber1.Value * _partNumber2.Value;
}