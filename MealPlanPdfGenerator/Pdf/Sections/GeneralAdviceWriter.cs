using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MealPlanPdfGenerator.Models;

namespace MealPlanPdfGenerator.Pdf.Sections
{
    public static class GeneralAdviceWriter
    {
        public static void Write(Document doc, FormEntry form)
        {
            doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            // Add general advice
            doc.Add(new Paragraph("Managing Eosinophilic Esophagitis:")
                .SetBold().SetFontSize(18).SetMarginBottom(0));
            doc.Add(new Paragraph("A Comprehensive Dietary Guide")
                .SetBold().SetFontSize(18).SetMarginTop(0).SetMarginBottom(15));

            doc.Add(new Paragraph("General Description of Eosinophilic Esophagitis (EoE):")
                .SetBold().SetMarginTop(10).SetMarginBottom(0));

            doc.Add(new Paragraph("Eosinophilic Esophagitis (EoE) is a chronic immune condition where eosinophils, a type of white blood cell, accumulate in the lining of the esophagus, leading to inflammation and damage. The condition is often triggered by certain food allergens and can cause significant symptoms such as difficulty swallowing (dysphagia), food impaction (when food gets stuck in the esophagus), chest pain, and heartburn. In children, EoE can also present as failure to thrive due to aversions to food or feeding difficulties. Management primarily involves identifying and eliminating trigger foods.")
                .SetMarginBottom(10));
            doc.Add(new Paragraph().Add(new Text("Symptoms:").SetBold()).Add(new Text("Difficulty swallowing, chest pain, food impaction, heartburn, vomiting, abdominal pain.")));

            doc.Add(new Paragraph("Tracking Symptoms:")
                .SetBold().SetMarginTop(10).SetMarginBottom(0));

            doc.Add(new Paragraph("Managing EoE requires diligent tracking of symptoms and potential triggers. It is recommended to keep a detailed food and symptom diary to help identify patterns over time. Tracking tools such as mobile apps or spreadsheets allow users to log meals, track symptoms, and generate reports that can be shared with healthcare providers, which can be highly effective in managing dietary changes and adjusting the treatment plan."));

            doc.Add(new Paragraph("Common Trigger Foods and Dietary Management For You:")
                .SetBold().SetMarginTop(10).SetMarginBottom(0));

            doc.Add(new Paragraph("EoE management often involves the elimination of specific food allergens that can trigger the disease. The six most common trigger foods include wheat, milk, eggs, soy, nuts, and fish. Below, we provide an overview of your triggers, detailing which foods to avoid, suggested replacements, potential nutritional deficiencies, and recommended blood tests and supplements."));

            int adviceCount = 0;

            if (form.Wheat || form.None || (!form.Wheat && !form.Milk && !form.Eggs && !form.Soy && !form.Nuts && !form.Fish))
            {
                WriteWheatAdvice(doc, form);
                adviceCount++;
            }

            if (form.Milk || form.None || (!form.Wheat && !form.Milk && !form.Eggs && !form.Soy && !form.Nuts && !form.Fish))
            {
                if (adviceCount == 1 || adviceCount == 4)
                {
                    doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                }

                WriteMilkAdvice(doc, form);
                adviceCount++;
            }

            if (form.Eggs || form.None || (!form.Wheat && !form.Milk && !form.Eggs && !form.Soy && !form.Nuts && !form.Fish))
            {
                if (adviceCount == 1 || adviceCount == 4)
                {
                    doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                }

                WriteEggsAdvice(doc);
                adviceCount++;
            }

            if (form.Soy || form.None || (!form.Wheat && !form.Milk && !form.Eggs && !form.Soy && !form.Nuts && !form.Fish))
            {
                if (adviceCount == 1 || adviceCount == 4)
                {
                    doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                }

                WriteSoyAdvice(doc, form);
                adviceCount++;
            }

            if (form.Nuts || form.None || (!form.Wheat && !form.Milk && !form.Eggs && !form.Soy && !form.Nuts && !form.Fish))
            {
                if (adviceCount == 1 || adviceCount == 4)
                {
                    doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                }

                WriteNutsAdvice(doc, form);
                adviceCount++;
            }

            if (form.Fish || form.None || (!form.Wheat && !form.Milk && !form.Eggs && !form.Soy && !form.Nuts && !form.Fish))
            {
                if (adviceCount == 1 || adviceCount == 4)
                {
                    doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                }

                WriteFishAdvice(doc);
            }
        }

        private static void WriteWheatAdvice(Document doc, FormEntry form)
        {
            doc.Add(new Paragraph("Wheat:")
                .SetBold().SetMarginBottom(0));

            List list = new List();
            list.SetListSymbol("• ");

            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Avoid: ").SetBold()).Add(new Text("Foods containing wheat, including bread, pasta, cereals, crackers, cakes, and cookies."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Replacements: ").SetBold()).Add(new Text("Gluten-free grains like quinoa, rice, millet, buckwheat, and oats. Many gluten-free products such as bread and pasta are available in stores."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Potential Deficiencies: ").SetBold()).Add(new Text("Eliminating wheat may reduce intake of B vitamins (especially folate), iron, and fiber."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Bloodwork: ").SetBold()).Add(new Text("Monitor folate, iron, and vitamin B12 levels every 6 months."))));

            string supps = form.Gender == "male"
            ? "A multivitamin that includes B vitamins and an iron supplement (8-11 mg iron) may be necessary."
            : "A multivitamin that includes B vitamins and an iron supplement (18 mg iron) may be necessary.";

            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Supplements: ").SetBold()).Add(supps)));

            doc.Add(list);
        }

        private static void WriteMilkAdvice(Document doc, FormEntry form)
        {
            doc.Add(new Paragraph("Milk:").SetBold().SetMarginTop(10).SetMarginBottom(0));

            List list = new List();
            list.SetListSymbol("• ");

            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Avoid: ").SetBold()).Add(new Text("Dairy products such as cow's milk, cheese, butter, yogurt, ice cream, and cream-based sauces."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Replacements: ").SetBold()).Add(new Text("Calcium-fortified plant-based milks (e.g., almond, soy, oat, or rice milk), and plant-based cheeses. Tofu can be a substitute for cream in some recipes."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Potential Deficiencies: ").SetBold()).Add(new Text("Risk of calcium and vitamin D deficiency."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Bloodwork: ").SetBold()).Add(new Text("Test calcium, vitamin D, and phosphorus levels every 6 months, with an increase in frequency if deficiencies are detected."))));

            string supps = form.Gender == "male"
                ? "Calcium (1,000 mg) and vitamin D (600-800 IU) supplements may be required if intake is insufficient."
                : "Calcium (1,200 mg) and vitamin D (600-800 IU) supplements may be required if intake is insufficient.";
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Supplements: ").SetBold()).Add(supps)));

            doc.Add(list);
        }

        private static void WriteEggsAdvice(Document doc)
        {
            doc.Add(new Paragraph("Eggs:").SetBold().SetMarginTop(10).SetMarginBottom(0));

            List list = new List();
            list.SetListSymbol("• ");

            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Avoid: ").SetBold()).Add(new Text("Eggs in all forms, including baked goods, mayonnaise, and egg-based sauces."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Replacements: ").SetBold()).Add(new Text("Use flaxseed or chia seed mixed with water as an egg replacement in baking (1 tbsp mixed with 3 tbsp of water = 1 egg). Applesauce and mashed bananas can also act as egg substitutes."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Potential Deficiencies: ").SetBold()).Add(new Text("Eliminating eggs may reduce intake of protein, biotin, and selenium."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Bloodwork: ").SetBold()).Add(new Text("Monitor selenium and protein levels every 6 months"))));

            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Supplements: ").SetBold()).Add(new Text("Protein supplements, especially plant-based, may help meet dietary needs."))));

            doc.Add(list);
        }

        private static void WriteSoyAdvice(Document doc, FormEntry form)
        {
            doc.Add(new Paragraph("Soy:").SetBold().SetMarginTop(10).SetMarginBottom(0));

            List list = new List();
            list.SetListSymbol("• ");

            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Avoid: ").SetBold()).Add(new Text("Tofu, soy milk, edamame, tempeh, miso, and any processed foods containing soy lecithin or soy protein isolate."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Replacements: ").SetBold()).Add(new Text("Legumes like lentils, chickpeas, and black beans are excellent substitutes for soy-based proteins. Non-soy plant-based protein powders are also available."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Potential Deficiencies: ").SetBold()).Add(new Text("Removing soy can result in lower intake of iron and protein."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Bloodwork: ").SetBold()).Add(new Text("Check iron and protein levels at least every 6 months, more often if symptoms of deficiency arise."))));

            string supps = form.Gender == "male"
                ? "Consider plant-based protein powders and iron supplements (8 mg iron) as needed."
                : "Consider plant-based protein powders and iron supplements (18 mg iron) as needed.";
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Supplements: ").SetBold()).Add(supps)));

            doc.Add(list);
        }

        private static void WriteNutsAdvice(Document doc, FormEntry form)
        {
            doc.Add(new Paragraph("Nuts:").SetBold().SetMarginTop(10).SetMarginBottom(0));

            List list = new List();
            list.SetListSymbol("• ");

            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Avoid: ").SetBold()).Add(new Text("Tree nuts (almonds, walnuts, cashews, etc.) and peanuts, as well as nut butters and products containing nuts."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Replacements: ").SetBold()).Add(new Text("Seeds like sunflower, chia, and pumpkin seeds provide healthy fats and proteins. Sunflower seed butter is a great substitute for peanut butter."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Potential Deficiencies: ").SetBold()).Add(new Text("Eliminating nuts can reduce intake of healthy fats, vitamin E, and magnesium."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Bloodwork: ").SetBold()).Add(new Text("Test for vitamin E and magnesium levels every 6-12 months, adjusting based on lab results."))));

            string supps = form.Gender == "male"
                ? "Vitamin E (15 mg) and magnesium (400-420 mg) supplements may be helpful."
                : "Vitamin E (15 mg) and magnesium (310-320 mg) supplements may be helpful.";
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Supplements: ").SetBold()).Add(supps)));

            doc.Add(list);
        }

        private static void WriteFishAdvice(Document doc)
        {
            doc.Add(new Paragraph("Fish:").SetBold().SetMarginTop(10).SetMarginBottom(0));

            List list = new List();
            list.SetListSymbol("• ");

            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Avoid: ").SetBold()).Add(new Text("All seafood including fish (salmon, tuna, cod) and shellfish (shrimp, crab, lobster)."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Replacements: ").SetBold()).Add(new Text("Plant-based sources of omega-3 fatty acids, such as flaxseeds, chia seeds, and algae-based supplements."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Potential Deficiencies: ").SetBold()).Add(new Text("Lack of omega-3 fatty acids and iodine."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Bloodwork: ").SetBold()).Add(new Text("Test omega-3 levels (as EPA and DHA) and iodine annually."))));
            list.Add((ListItem)new ListItem().Add(new Paragraph().Add(new Text("Supplements: ").SetBold()).Add(new Text("Omega-3 supplements derived from algae (500-1,000 mg EPA+DHA) can provide similar benefits to fish oil."))));

            doc.Add(list);
        }
    }
} 