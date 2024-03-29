﻿using System;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Farm;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using FluentAssertions;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests
{
    public class TestDataHelper
    {
        public static Checklist.Checklist ConstructChecklist()
        {
            var checklist = new Checklist.Checklist(1)
                            .AddRubric("R1", new RubricResult("R1", "R1", "R1")
                                             .AddChild("R1,P1", new PointResult("R1,P1", "P1", "P1"))
                                             .AddChild("R1,P2", new PointResult("R1,P2", "P2", "P2")))
                            .AddRubric("R2", new RubricResult("R2", "R2", "R2")
                                             .AddChild("R2,G1", new GroupResult("R2,G1", "G1", "G1")
                                                                .AddChild("R2,G1,P1", new PointResult("R2,G1,P1", "P1", "P1"))
                                                                .AddChild("R2,G1,P2", new PointResult("R2,G1,P2", "P2", "P2"))
                                                                .AddChild("R2,G1,P3", new PointResult("R2,G1,P3", "P3", "P3")))
                                             .AddChild("R2,G2", new GroupResult("R2,G2", "G2", "G2")
                                                                .AddChild("R2,G2,SG1", new GroupResult("R2,G2,SG1", "SG1", "SG1")
                                                                                       .AddChild("R2,G2,SG1,P1", new PointResult("R2,G2,SG1,P1", "P1", "P1"))
                                                                                       .AddChild("R2,G2,SG1,P2", new PointResult("R2,G2,SG1,P2", "P2", "P2"))
                                                                                       .AddChild("R2,G2,SG1,P3", new PointResult("R2,G2,SG1,P3", "P3", "P3"))
                                                                                       .AddChild("R2,G2,SG1,P4", new PointResult("R2,G2,SG1,P4", "P4", "P4")))
                                                                .AddChild("R2,G2,SG2", new GroupResult("R2,G2,SG2", "SG2", "SG2")
                                                                                       .AddChild("R2,G2,SG2,P1", new PointResult("R2,G2,SG2,P1", "P1", "P1"))
                                                                                       .AddChild("R2,G2,SG2,P2", new PointResult("R2,G2,SG2,P2", "P2", "P2")))));
            return checklist;
        }

        public static Signature ConstructSignature(string signatory)
        {
            return new Signature(signatory,
                                 "",
                                 false,
                                 1,
                                 "[{\"color\":\"#5c2d91\",\"points\":[{\"time\":1611238093427,\"x\":186.5,\"y\":132.79998779296875},{\"time\":1611238093530,\"x\":180.90000915527344,\"y\":134.40005493164062},{\"time\":1611238093644,\"x\":169.6999969482422,\"y\":141.60000610351562},{\"time\":1611238093660,\"x\":156.10000610351562,\"y\":149.60000610351562},{\"time\":1611238093676,\"x\":137.6999969482422,\"y\":155.19998168945312},{\"time\":1611238093693,\"x\":119.30000305175781,\"y\":158.40005493164062},{\"time\":1611238093710,\"x\":104.90000915527344,\"y\":159.19998168945312},{\"time\":1611238093726,\"x\":96.90000915527344,\"y\":157.60000610351562},{\"time\":1611238093760,\"x\":104.10000610351562,\"y\":128},{\"time\":1611238093776,\"x\":126.5,\"y\":100.79998779296875},{\"time\":1611238093792,\"x\":146.5,\"y\":86.40005493164062},{\"time\":1611238093809,\"x\":217.70001220703125,\"y\":51.199981689453125},{\"time\":1611238093826,\"x\":233.70001220703125,\"y\":48.79998779296875},{\"time\":1611238093861,\"x\":235.30001831054688,\"y\":54.399993896484375},{\"time\":1611238093877,\"x\":230.5,\"y\":76},{\"time\":1611238093894,\"x\":198.5,\"y\":130.40005493164062},{\"time\":1611238093911,\"x\":186.5,\"y\":153.60000610351562},{\"time\":1611238093928,\"x\":183.3000030517578,\"y\":165.60000610351562},{\"time\":1611238093946,\"x\":192.90000915527344,\"y\":174.4000244140625},{\"time\":1611238093962,\"x\":215.30001831054688,\"y\":174.4000244140625},{\"time\":1611238093978,\"x\":259.3000183105469,\"y\":160.79998779296875},{\"time\":1611238093994,\"x\":302.5,\"y\":144.79998779296875},{\"time\":1611238094010,\"x\":314.5,\"y\":143.19998168945312},{\"time\":1611238094048,\"x\":312.8999938964844,\"y\":161.60000610351562},{\"time\":1611238094064,\"x\":305.70001220703125,\"y\":182.4000244140625},{\"time\":1611238094080,\"x\":300.1000061035156,\"y\":196.79998779296875},{\"time\":1611238094096,\"x\":299.3000183105469,\"y\":202.4000244140625},{\"time\":1611238094148,\"x\":304.8999938964844,\"y\":202.4000244140625},{\"time\":1611238094180,\"x\":285.70001220703125,\"y\":193.5999755859375},{\"time\":1611238094196,\"x\":248.89999389648438,\"y\":185.5999755859375},{\"time\":1611238094212,\"x\":205.70001220703125,\"y\":184},{\"time\":1611238094230,\"x\":149.6999969482422,\"y\":178.4000244140625},{\"time\":1611238094248,\"x\":104.10000610351562,\"y\":178.4000244140625},{\"time\":1611238094266,\"x\":87.30000305175781,\"y\":178.4000244140625},{\"time\":1611238094284,\"x\":77.69999694824219,\"y\":178.4000244140625},{\"time\":1611238094318,\"x\":79.30000305175781,\"y\":172},{\"time\":1611238094335,\"x\":91.30000305175781,\"y\":157.60000610351562},{\"time\":1611238094352,\"x\":115.30000305175781,\"y\":131.19998168945312},{\"time\":1611238094368,\"x\":144.90000915527344,\"y\":110.40005493164062},{\"time\":1611238094385,\"x\":153.6999969482422,\"y\":109.60000610351562},{\"time\":1611238094402,\"x\":164.90000915527344,\"y\":114.40005493164062},{\"time\":1611238094419,\"x\":180.90000915527344,\"y\":125.60000610351562},{\"time\":1611238094436,\"x\":200.90000915527344,\"y\":137.60000610351562},{\"time\":1611238094452,\"x\":222.5,\"y\":145.60000610351562},{\"time\":1611238094468,\"x\":236.10000610351562,\"y\":145.60000610351562},{\"time\":1611238094484,\"x\":250.5,\"y\":142.40005493164062},{\"time\":1611238094500,\"x\":264.1000061035156,\"y\":134.40005493164062},{\"time\":1611238094517,\"x\":267.3000183105469,\"y\":130.40005493164062},{\"time\":1611238094551,\"x\":264.8999938964844,\"y\":122.40005493164062},{\"time\":1611238094568,\"x\":257.70001220703125,\"y\":117.60000610351562},{\"time\":1611238094632,\"x\":295.3000183105469,\"y\":119.19998168945312},{\"time\":1611238094648,\"x\":348.8999938964844,\"y\":119.19998168945312},{\"time\":1611238094665,\"x\":404.8999938964844,\"y\":109.60000610351562},{\"time\":1611238094683,\"x\":456.8999938964844,\"y\":95.19998168945312},{\"time\":1611238094700,\"x\":473.70001220703125,\"y\":91.19998168945312},{\"time\":1611238094756,\"x\":461.70001220703125,\"y\":92},{\"time\":1611238094772,\"x\":454.5,\"y\":92.79998779296875}]}]",
                                 "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAArwAAAHMCAYAAAA3cHsfAAAgAElEQVR4Xu3df2xl6X3X8e / 32J4N3V1lQoKgpWK9QoR0x846qIAEgfUKKgoiGq9KUTI2Gq + USpXa7rW3qVIJwXikVi1qGl9HraB / 0PWo9hA1LeNpK1QQaDwiQJGo1iN7tiW0rEdt1KJS4q1nm23G9 / mi55x77GOPPb4 / znOfe899X6lqkjnnec55fY9nPvfxc55HhQ8CCCCAAAIIIIAAAhUW0ArfG7eGAAIIIIAAAggggIAQeHkIEEAAAQQQQAABBCotQOCtdHm5OQQQQAABBBBAAAECL88AAggggAACCCCAQKUFCLyVLi83hwACCCCAAAIIIEDg5RlAAAEEEEAAAQQQqLQAgbfS5eXmEEAAAQQQQAABBAi8PAMIIIAAAggggAAClRYg8Fa6vNwcAggggAACCCCAAIGXZwABBBBAAAEEEECg0gIE3kqXl5tDAAEEEEAAAQQQIPDyDCCAAAIIIIAAAghUWoDAW + nycnMIIIAAAggggAACBF6eAQQQQAABBBBAAIFKCxB4K11ebg4BBBBAAAEEEECAwMszgAACCCCAAAIIIFBpAQJvpcvLzSGAAAIIIIAAAggQeHkGEEAAAQQQQAABBCotQOCtdHm5OQQQQAABBBBAAAECL88AAggggAACCCCAQKUFCLyVLi83hwACCCCAAAIIIEDg5RlAAAEEEEAAAQQQqLQAgbfS5eXmEEAAAQQQQAABBAi8PAMIIIAAAggggAAClRYg8Fa6vNwcAggggAACCCCAAIGXZwABBBBAAAEEEECg0gIE3kqXl5tDAAEEEEAAAQQQIPDyDCCAAAIIIIAAAghUWoDAW + nycnMIIIAAAggggAACBF6eAQQQQAABBBBAAIFKCxB4K11ebg4BBBBAAAEEEECAwMszgAACCCCAAAIIIFBpAQJvpcvLzSGAAAIIIIAAAggQeHkGEEAAAQQQQAABBCotQOCtdHm5OQQQQAABBBBAAAECL88AAggggAACCCCAQKUFCLyVLi83hwACCCCAAAIIIEDg5RlAAAEEEEAAAQQQqLQAgbfS5eXmEEAAAQQQQAABBAi8PAMIIIAAAggggAAClRYg8Fa6vNwcAggggAACCCCAAIGXZwABBBBAAAEEEECg0gIE3kqXl5tDAAEEEEAAAQQQIPDyDCCAAAIIIIAAAghUWoDAW + nycnMIIIAAAggggAACBF6eAQQQQAABBBBAAIFKCxB4K11ebm4QBObHly82nh158eS1NhqNdy78seyu7i7uDcJ9cI0IIIAAAgj0qwCBt18rw3VVWuCTH1keHxnTmphMq + qUmTzwN6wqzxVv3MxEVUVMrq / t1JYqjcLNIYAAAgggEEiAwBsIlmYROCngR3IfPZ1cFrUFUXleTVdF3MbazuJm8Vh / 3MEzMiUqF8WSKTO7qomOm1h9fXthEVkEEEAAAQQQaE + AwNueF0cj0LbAlReWp3REr4rIqyK6oeZWT4bcJzXqR4NHR5MNUXnRnC6u33 + t3vZFcAICCCCAAAJDLEDgHeLic + thBeYml2dMdFlNHzh1qze3F1c77XFuYmVJVK6Jyd21ndp0p + 1wHgIIIIAAAsMoQOAdxqpzz8EEmqOxl01tIQ26zi3cfGtxq9sOZydXNlTkMoG3W0nORwABBBAYRgEC7zBWnXsuXWBuYnnaVK + q6LyJ3Gh32sJ5F3Rlov5eovqUmeys79QmzzueP0cAAQQQQACBIwECL08DAl0IzF5auepfQktXWhC5MbbvFspeRszPAU5Gkjf9ZZrZr6 / vLHx7F5fMqQgggAACCAydAIF36ErODXcr0Jy2kAZd8yuJmdZHH7p62UE3v04 / eiya3En / O3N4uy0f5yOAAAIIDKEAgXcIi84tdybQXDv3WjptweSBqVu6sC8boYIugbezOnEWAggggAACJwUIvDwTCJwj4KctqNq8qE7nQbebFRfaBWeEt10xjkcAAQQQQOC4AIGXJwKBUwSyzR + SmqnNq / hNH + S2mqu3s35uWbDFwOuvY327NlNW27SDAAIIIIDAMAgQeIehytxjywLFaQv + JP8iWuORW / riby7uttxIyQeeCLw31rdr8yV3QXMIIIAAAghUWoDAW + nycnOtClyZXLmcmN / yV6dN7B3 / ItrBgVuNGXTza / cbWIgkt9L / bnJ9bae21Op9cRwCCCCAAAIIiBB4eQqGVsBPW3j0dHJZElvKpi3YO + KSpbF3G6uhX0RrB / 1wlzUCbztsHIsAAggggMChAIGXh2HoBPL5uX5ZMRG9GONFtHbQ5yZW6qJSy85xr6xtL260cz7HIoAAAgggMOwCBN5hfwKG6P5Pzs / 1a9qKunq / B8i5iZVNUXkpjbsN97EytioeorJzqwgggAACCDClgWeg + gLpS1 + i1 / z83PRufdAVtxRjxYVOtOcmVyy7bHtnfXvhYidtcA4CCCCAAALDLMAI7zBXv + L3Xtz2NwuMcsMarj5II6QsSVbxh5TbQwABBBDoiQCBtyfMdNJLgTToNl9Ey4Nu7KXFOr3 / 4vxdJ + 7VXm540ek1cx4CCCCAAAL9JkDg7beKcD0dCTRfRLtqagt + xYVBD7o5wuzEyq6qPOf / ++i++0A / rR7RUaE4CQEEEEAAgQgCBN4I6HRZnkBVg64XuvLC8lQykryZh3c2nCjvuaElBBBAAIHhEiDwDle9K3O3VQ66eZGOLUdm7uVBecmuMg8ZN4IAAgggUBkBAm9lSjk8N + Ln6Gpidb + GbnrXJisHB67eD7uilVmFw + kMJvfWdmpTZbZNWwgggAACCAyTAIF3mKo94PdapZfRzitFcToDL6udp8WfI4AAAggg8GQBAi9PSN8LzE0uz5jocpVeRjsPPZ / O4NfeHdu3cV5WO0 + MP0cAAQQQQOBsAQIvT0ffCpy2YYRzbmGQ1tHtFDefzuDXDuZltU4VOQ8BBBBAAIFMgMDLk9B3An4L4NFRfaOwM9o9EbcwLC9tHZvOwFbCffd8ckEIIIAAAoMnQOAdvJpV9op90B0Z02sqOu9v0kwemLqlYdtsYfbSyoYmctlMvra + U / vTlS04N4YAAggggECPBAi8PYKmm7MFmkuM1URtwa + 84OetmtjCsAVdL5SObo8lb6daJp9b26n9EM8OAggggAACCHQnQODtzo + zuxSYm1jxQXcpD7pqWh996OrD + pLW7ET9TVWd8qPb6zu1dMc4PggggAACCCDQnQCBtzs / zu5Q4LQlxsb23cKwBl3PmL6kp8mdbHSXjSY6fLQ4DQEEEEAAgccECLw8FD0V8KEuXWJMNd1Iwa9C0Hjklqq2aUQnqHOT9a9lI91ye327NtNJG5yDAAIIIIAAAo8LEHh5Knoi4Fce0BGt5S + kiclQrbxwHnJxG + GDR + 55vgCcJ8afI4AAAggg0LoAgbd1K47sQCBbYiy5KipL6ekm95y6 + jC + kHYW34kX1a6v7dQyKz4IIIAAAgggUIoAgbcURho5KeBXXnj0dHJZE6sXX0gjzD3 + rMxN1O / 4NYf9i2pjD93UMM9j5icJAQQQQACBEAIE3hCqQ97myRfSxGRl9KFbIsidFnaPXlRz4l5l5HvIf3i4fQQQQACBIAIE3iCsw9noya2A / ctXY / tunqB79vOQL0MmJnfXdmrTw / nkcNcIIIAAAgiEFSDwhvUditY / 9ZfrH00uyGLhhbS7zrmFm28tbg0FQIc3OTexsiQq1 / zpvKjWISKnIYAAAggg0IIAgbcFJA45XSCdp / usLhe3AlZ1C2vbixuYPVkg3V3uWX27uQzZjfXtWrqdMh8EEEAAAQQQKF + AwFu + aeVbPAq6MuMDmzj5I03sJ0b27aeYvtBa + fNlyPw2yo1HNsUyZK25cRQCCCCAAAKdCBB4O1Eb4nNmL31hQRN3LV95QVyyNPZuY5Wg2 / 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"
            );
        }

        public static void ChecklistTreeStructureShouldBeConsistent(Checklist.Checklist checklist)
        {
            checklist.Should().NotBeNull();

            checklist.FarmInspectionId.Should().Be(1);
            checklist.Rubrics.Count.Should().Be(2);
            checklist.Rubrics["R1"].Children.Count.Should().Be(2);
            checklist.Rubrics["R1"].Children.Should().ContainKeys("R1,P1", "R1,P2");
            checklist.Rubrics["R1"].NumGroups.Should().Be(0);
            checklist.Rubrics["R1"].NumPoints.Should().Be(2);
            checklist.Rubrics["R2"].Children.Count.Should().Be(2);
            checklist.Rubrics["R2"].Children.Should().ContainKeys("R2,G1", "R2,G2");
            checklist.Rubrics["R2"].NumGroups.Should().Be(2);
            checklist.Rubrics["R2"].NumPoints.Should().Be(0);

            checklist.Rubrics["R1"].Children["R1,P1"].NumGroups.Should().Be(0);
            checklist.Rubrics["R1"].Children["R1,P1"].NumPoints.Should().Be(0);
            checklist.Rubrics["R1"].Children["R1,P1"].ConjunctElementCode.Should().Be("R1,P1");
            checklist.Rubrics["R1"].Children["R1,P1"].ElementCode.Should().Be("P1");
            checklist.Rubrics["R1"].Children["R1,P1"].ShortName.Should().Be("P1");
            checklist.Rubrics["R1"].Children["R1,P1"].Parent.Should().NotBeNull();
            checklist.Rubrics["R1"].Children["R1,P1"].Parent.ConjunctElementCode.Should().Be("R1");
            checklist.Rubrics["R1"].Children["R1,P1"].Parent.ElementCode.Should().Be("R1");
            checklist.Rubrics["R1"].Children["R1,P1"].Parent.ShortName.Should().Be("R1");
            checklist.Rubrics["R1"].Children["R1,P2"].NumGroups.Should().Be(0);
            checklist.Rubrics["R1"].Children["R1,P2"].NumPoints.Should().Be(0);
            checklist.Rubrics["R1"].Children["R1,P2"].ConjunctElementCode.Should().Be("R1,P2");
            checklist.Rubrics["R1"].Children["R1,P2"].ElementCode.Should().Be("P2");
            checklist.Rubrics["R1"].Children["R1,P2"].ShortName.Should().Be("P2");
            checklist.Rubrics["R1"].Children["R1,P2"].Parent.Should().NotBeNull();
            checklist.Rubrics["R1"].Children["R1,P2"].Parent.ConjunctElementCode.Should().Be("R1");
            checklist.Rubrics["R1"].Children["R1,P2"].Parent.ElementCode.Should().Be("R1");
            checklist.Rubrics["R1"].Children["R1,P2"].Parent.ShortName.Should().Be("R1");

            checklist.Rubrics["R2"].Children["R2,G1"].NumGroups.Should().Be(0);
            checklist.Rubrics["R2"].Children["R2,G1"].NumPoints.Should().Be(3);
            checklist.Rubrics["R2"].Children["R2,G1"].ConjunctElementCode.Should().Be("R2,G1");
            checklist.Rubrics["R2"].Children["R2,G1"].ElementCode.Should().Be("G1");
            checklist.Rubrics["R2"].Children["R2,G1"].ShortName.Should().Be("G1");
            checklist.Rubrics["R2"].Children["R2,G1"].Parent.Should().NotBeNull();
            checklist.Rubrics["R2"].Children["R2,G1"].Parent.ConjunctElementCode.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G1"].Parent.ElementCode.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G1"].Parent.ShortName.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G1"].Children.Count.Should().Be(3);
            checklist.Rubrics["R2"].Children["R2,G1"].Children.Should().ContainKeys("R2,G1,P1", "R2,G1,P2", "R2,G1,P3");

            checklist.Rubrics["R2"].Children["R2,G2"].NumGroups.Should().Be(2);
            checklist.Rubrics["R2"].Children["R2,G2"].NumPoints.Should().Be(0);
            checklist.Rubrics["R2"].Children["R2,G2"].ConjunctElementCode.Should().Be("R2,G2");
            checklist.Rubrics["R2"].Children["R2,G2"].ElementCode.Should().Be("G2");
            checklist.Rubrics["R2"].Children["R2,G2"].ShortName.Should().Be("G2");
            checklist.Rubrics["R2"].Children["R2,G2"].Parent.Should().NotBeNull();
            checklist.Rubrics["R2"].Children["R2,G2"].Parent.ConjunctElementCode.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G2"].Parent.ElementCode.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G2"].Parent.ShortName.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G2"].Children.Count.Should().Be(2);
            checklist.Rubrics["R2"].Children["R2,G2"].Children.Should().ContainKeys("R2,G2,SG1", "R2,G2,SG2");

            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG1"].NumGroups.Should().Be(0);
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG1"].NumPoints.Should().Be(4);
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG1"].Children.Count.Should().Be(4);
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG1"].Children.Should().ContainKeys("R2,G2,SG1,P1", "R2,G2,SG1,P2", "R2,G2,SG1,P3", "R2,G2,SG1,P4");
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG2"].NumGroups.Should().Be(0);
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG2"].NumPoints.Should().Be(2);
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG2"].Children.Count.Should().Be(2);
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG2"].Children.Should().ContainKeys("R2,G2,SG2,P1", "R2,G2,SG2,P2");
        }

        public static Checklist.Checklist AllOk(Checklist.Checklist checklist)
        {
            checklist.SetOutcome("R1,P1", InspectionOutcome.Ok);
            checklist.SetOutcome("R1,P2", InspectionOutcome.Ok);
            checklist.SetOutcome("R1", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G2,SG1,P1", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G2,SG1,P2", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G2,SG1,P3", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G2,SG1,P4", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G2,SG1", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G2,SG2,P1", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G2,SG2,P2", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G2,SG2", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G2", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G1,P1", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G1,P2", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G1,P3", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G1", InspectionOutcome.Ok);
            checklist.SetOutcome("R2", InspectionOutcome.Ok);
            return checklist;
        }

        public const int FarmInspectionId = 1;
        public static readonly Guid InspectionId = Guid.NewGuid();
        public const long ChecklistId = 1;
        public static readonly Inspection.Domain Domaine_PER_Grandes_Cultures = new Inspection.Domain(1, "PER grandes cultures");
        public static readonly Campaign Campagne_été_2020 = new Campaign(1, "Campagne été 2020", 2020);
        public const string EmptyComment = "";
        public const long FarmId = 1;
        
        public static Inspection.Inspection ConstructInspection()
        {
            return new Inspection.Inspection(
                new Inspection.Inspection.InitObject
                {
                    FarmInspectionId = FarmInspectionId,
                    InspectionId = InspectionId,
                    Domain = Domaine_PER_Grandes_Cultures,
                    Campaign = Campagne_été_2020,
                    Reason = InspectionReason.Routine,
                    Comment = EmptyComment,
                    ChecklistId = ChecklistId,
                    FarmId = FarmId
                });
        }

        public static void InspectionShouldBeSuchAsConstructed(Inspection.Inspection inspection)
        {
            inspection.Should().NotBeNull();
            inspection.FarmInspectionId.Should().Be(FarmInspectionId);
            inspection.Domain.Should().Be(Domaine_PER_Grandes_Cultures);
            inspection.Campaign.Should().Be(Campagne_été_2020);
            inspection.Reason.Should().Be(InspectionReason.Routine);
            inspection.ChecklistId.Should().Be(ChecklistId);
        }

        public static Farm.Farm ConstructFarm()
        {
            return new Farm.Farm(141)
            {
                Ktidb = "JU67060010",
                FarmName = "Frund Vincent",
                Address = "Petit-Bâle, 4, 2825 Courchapoix",
                FarmType = "Exploitation à l’année",
                FarmTypeCode = 1,
                Email = "isabelle.lobsiger@gmail.com",
                PhoneNumber = "079 343 04 52",
                AgriculturalArea = "0",
                NonAgriculturalArea = "0",
                BovineStandardUnits = "0.0",
                BovineStandardUnitsFromBdta = "0.0",
                Badges = new[]
                {
                    new Badge { Category = "btsraus", Name = "SST", Title = "SST" },
                    new Badge { Category = "btsraus", Name = "SRPA", Title = "SRPA" },
                }
            };
        }

        public static void FarmShouldBeSuchAsConstructed(Farm.Farm farm)
        {
            farm.Should().NotBeNull();
            farm.Id.Should().Be(141);
            farm.Ktidb.Should().Be("JU67060010");
            farm.FarmName.Should().Be("Frund Vincent");
            farm.Address.Should().Be("Petit-Bâle, 4, 2825 Courchapoix");
            farm.FarmType.Should().Be("Exploitation à l’année");
            farm.FarmTypeCode.Should().Be(1);
            farm.Email.Should().Be("isabelle.lobsiger@gmail.com");
            farm.PhoneNumber.Should().Be("079 343 04 52");
            farm.AgriculturalArea.Should().Be("0");
            farm.NonAgriculturalArea.Should().Be("0");
            farm.BovineStandardUnits.Should().Be("0.0");
            farm.BovineStandardUnitsFromBdta.Should().Be("0.0");
            farm.Badges.Should().HaveCount(2);
            farm.Badges[0].Category.Should().Be("btsraus");
            farm.Badges[0].Name.Should().Be("SST");
            farm.Badges[0].Title.Should().Be("SST");
            farm.Badges[1].Category.Should().Be("btsraus");
            farm.Badges[1].Name.Should().Be("SRPA");
            farm.Badges[1].Title.Should().Be("SRPA");
        }
    }
}