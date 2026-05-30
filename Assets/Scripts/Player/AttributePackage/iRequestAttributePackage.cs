using System;

public interface iRequestAttributePackage {
    public EntityAttributePackage attributePackage { get; set; }
    public event Action attributePackageRequested;
}
