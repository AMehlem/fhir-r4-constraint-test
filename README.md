# ConstraintTest

This repository uses the library HL7.Fhir.Specification.R4 version 4.3.0 to validate a XML file. The file ("Resources/Bundle.xml") should be valid, but the validation fails with one error (and 14 irrelevant issues). The following constraint fails although the XML contains three extensions with url 'leitsymptomatik' and none of these contain the code value 'pi':
```
code.extension('https://fhir.gevko.de/StructureDefinition/EVO_EX_HLM_Leitsymptomatik').extension('leitsymptomatik').exists() and
(code.extension('https://fhir.gevko.de/StructureDefinition/EVO_EX_HLM_Leitsymptomatik').extension('leitsymptomatik')[0].valueCoding.code='pi' or 
code.extension('https://fhir.gevko.de/StructureDefinition/EVO_EX_HLM_Leitsymptomatik').extension('leitsymptomatik')[1].valueCoding.code='pi' or 
code.extension('https://fhir.gevko.de/StructureDefinition/EVO_EX_HLM_Leitsymptomatik').extension('leitsymptomatik')[2].valueCoding.code='pi')  implies 
code.extension('https://fhir.gevko.de/StructureDefinition/EVO_EX_HLM_Leitsymptomatik').extension('leitsymptomatik').count()=1
```
This constraint is defined in the file Resources/StructureDefinitions/dependencies/de.gevko.evo.hlm#1.2.0/package/EVO_PR_HLM_Condition_Diagnoseschluessel.json with key "Codes-Diagnosegruppe-1". The profile is also available on simplifier: https://simplifier.net/packages/de.gevko.evo.hlm/1.2.0/files/669999.

The following Xml-part fails validation:
```
<extension url="https://fhir.gevko.de/StructureDefinition/EVO_EX_HLM_Leitsymptomatik">
    <extension url="leitsymptomatik">
        <valueCoding>
            <system value="https://fhir.gevko.de/CodeSystem/EVO_CS_HLM_Leitsymptomatik"/>
            <code value="c"/>
            <display value="Leitsymptomatik c"/>
        </valueCoding>
    </extension>
    <extension url="leitsymptomatik">
        <valueCoding>
            <system value="https://fhir.gevko.de/CodeSystem/EVO_CS_HLM_Leitsymptomatik"/>
            <code value="b"/>
            <display value="Leitsymptomatik b"/>
        </valueCoding>
    </extension>
    <extension url="leitsymptomatik">
        <valueCoding>
            <system value="https://fhir.gevko.de/CodeSystem/EVO_CS_HLM_Leitsymptomatik"/>
            <code value="a"/>
            <display value="Leitsymptomatik a"/>
        </valueCoding>
    </extension>
</extension>
```
This part can be found in the example bundle by searching for "https://fhir.gevko.de/CodeSystem/EVO_CS_HLM_Leitsymptomatik".
In my opinion, the constraint should be satisfied. The validation says it's not. Interestingly, if only one extension "leitsymptomatik" is present, the validation succeeds.
