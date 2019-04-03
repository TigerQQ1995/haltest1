using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace haltest1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        public partial class HDevelopExport
        {
            public HTuple hv_ExpDefaultWinHandle;

            public void HDevelopStop()
            {
                MessageBox.Show("Press button to continue", "Program stop");
            }

            // Procedures 
            // External procedures 
            // Chapter: Graphics / Text
            // Short Description: This procedure writes a text message. 
            public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
                HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
            {



                // Local iconic variables 

                // Local control variables 

                HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
                HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
                HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
                HTuple hv_WidthWin = new HTuple(), hv_HeightWin = null;
                HTuple hv_MaxAscent = null, hv_MaxDescent = null, hv_MaxWidth = null;
                HTuple hv_MaxHeight = null, hv_R1 = new HTuple(), hv_C1 = new HTuple();
                HTuple hv_FactorRow = new HTuple(), hv_FactorColumn = new HTuple();
                HTuple hv_UseShadow = null, hv_ShadowColor = null, hv_Exception = new HTuple();
                HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
                HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
                HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
                HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
                HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
                HTuple hv_CurrentColor = new HTuple();
                HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
                HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
                HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
                HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
                HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

                // Initialize local and output iconic variables 
                //This procedure displays text in a graphics window.
                //
                //Input parameters:
                //WindowHandle: The WindowHandle of the graphics window, where
                //   the message should be displayed
                //String: A tuple of strings containing the text message to be displayed
                //CoordSystem: If set to 'window', the text position is given
                //   with respect to the window coordinate system.
                //   If set to 'image', image coordinates are used.
                //   (This may be useful in zoomed images.)
                //Row: The row coordinate of the desired text position
                //   If set to -1, a default value of 12 is used.
                //Column: The column coordinate of the desired text position
                //   If set to -1, a default value of 12 is used.
                //Color: defines the color of the text as string.
                //   If set to [], '' or 'auto' the currently set color is used.
                //   If a tuple of strings is passed, the colors are used cyclically
                //   for each new textline.
                //Box: If Box[0] is set to 'true', the text is written within an orange box.
                //     If set to' false', no box is displayed.
                //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
                //       the text is written in a box of that color.
                //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
                //       'true' -> display a shadow in a default color
                //       'false' -> display no shadow (same as if no second value is given)
                //       otherwise -> use given string as color string for the shadow color
                //
                //Prepare window
                HOperatorSet.GetRgb(hv_ExpDefaultWinHandle, out hv_Red, out hv_Green, out hv_Blue);
                HOperatorSet.GetPart(hv_ExpDefaultWinHandle, out hv_Row1Part, out hv_Column1Part,
                    out hv_Row2Part, out hv_Column2Part);
                HOperatorSet.GetWindowExtents(hv_ExpDefaultWinHandle, out hv_RowWin, out hv_ColumnWin,
                    out hv_WidthWin, out hv_HeightWin);
                HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
                //
                //default settings
                if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
                {
                    hv_Row_COPY_INP_TMP = 12;
                }
                if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
                {
                    hv_Column_COPY_INP_TMP = 12;
                }
                if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
                {
                    hv_Color_COPY_INP_TMP = "";
                }
                //
                hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
                //
                //Estimate extentions of text depending on font size.
                HOperatorSet.GetFontExtents(hv_ExpDefaultWinHandle, out hv_MaxAscent, out hv_MaxDescent,
                    out hv_MaxWidth, out hv_MaxHeight);
                if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
                {
                    hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                    hv_C1 = hv_Column_COPY_INP_TMP.Clone();
                }
                else
                {
                    //Transform image to window coordinates
                    hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                    hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                    hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                    hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
                }
                //
                //Display text box depending on text size
                hv_UseShadow = 1;
                hv_ShadowColor = "gray";
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
                {
                    if (hv_Box_COPY_INP_TMP == null)
                        hv_Box_COPY_INP_TMP = new HTuple();
                    hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                    hv_ShadowColor = "#f28d26";
                }
                if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                    1))) != 0)
                {
                    if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                    {
                        //Use default ShadowColor set above
                    }
                    else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                        "false"))) != 0)
                    {
                        hv_UseShadow = 0;
                    }
                    else
                    {
                        hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                        //Valid color?
                        try
                        {
                            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                                1));
                        }
                        // catch (Exception) 
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                            hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                            throw new HalconException(hv_Exception);
                        }
                    }
                }
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
                {
                    //Valid color?
                    try
                    {
                        HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                            0));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                    //Calculate box extents
                    hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                    hv_Width = new HTuple();
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, hv_String_COPY_INP_TMP.TupleSelect(
                            hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                        hv_Width = hv_Width.TupleConcat(hv_W);
                    }
                    hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                        ));
                    hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                    hv_R2 = hv_R1 + hv_FrameHeight;
                    hv_C2 = hv_C1 + hv_FrameWidth;
                    //Display rectangles
                    HOperatorSet.GetDraw(hv_ExpDefaultWinHandle, out hv_DrawMode);
                    HOperatorSet.SetDraw(hv_ExpDefaultWinHandle, "fill");
                    //Set shadow color
                    HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_ShadowColor);
                    if ((int)(hv_UseShadow) != 0)
                    {
                        HOperatorSet.DispRectangle1(hv_ExpDefaultWinHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1,
                            hv_C2 + 1);
                    }
                    //Set box color
                    HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                        0));
                    HOperatorSet.DispRectangle1(hv_ExpDefaultWinHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                    HOperatorSet.SetDraw(hv_ExpDefaultWinHandle, hv_DrawMode);
                }
                //Write text.
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                        )));
                    if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                        "auto")))) != 0)
                    {
                        HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_CurrentColor);
                    }
                    else
                    {
                        HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red, hv_Green, hv_Blue);
                    }
                    hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                    HOperatorSet.SetTposition(hv_ExpDefaultWinHandle, hv_Row_COPY_INP_TMP, hv_C1);
                    HOperatorSet.WriteString(hv_ExpDefaultWinHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index));
                }
                //Reset changed window settings
                HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red, hv_Green, hv_Blue);
                HOperatorSet.SetPart(hv_ExpDefaultWinHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                    hv_Column2Part);

                return;
            }

            // Chapter: Graphics / Text
            // Short Description: This procedure displays 'Click 'Run' to continue' in the lower right corner of the screen. 
            public void disp_continue_message(HTuple hv_WindowHandle, HTuple hv_Color, HTuple hv_Box)
            {



                // Local iconic variables 

                // Local control variables 

                HTuple hv_ContinueMessage = null, hv_Row = null;
                HTuple hv_Column = null, hv_Width = null, hv_Height = null;
                HTuple hv_Ascent = null, hv_Descent = null, hv_TextWidth = null;
                HTuple hv_TextHeight = null;
                // Initialize local and output iconic variables 
                //This procedure displays 'Press Run (F5) to continue' in the
                //lower right corner of the screen.
                //It uses the procedure disp_message.
                //
                //Input parameters:
                //WindowHandle: The window, where the text shall be displayed
                //Color: defines the text color.
                //   If set to '' or 'auto', the currently set color is used.
                //Box: If set to 'true', the text is displayed in a box.
                //
                hv_ContinueMessage = "Press Run (F5) to continue";
                HOperatorSet.GetWindowExtents(hv_ExpDefaultWinHandle, out hv_Row, out hv_Column,
                    out hv_Width, out hv_Height);
                HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, (" " + hv_ContinueMessage) + " ",
                    out hv_Ascent, out hv_Descent, out hv_TextWidth, out hv_TextHeight);
                disp_message(hv_ExpDefaultWinHandle, hv_ContinueMessage, "window", (hv_Height - hv_TextHeight) - 12,
                    (hv_Width - hv_TextWidth) - 12, hv_Color, hv_Box);

                return;
            }

            // Chapter: Graphics / Text
            // Short Description: Set font independent of OS 
            public void set_display_font(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font,
                HTuple hv_Bold, HTuple hv_Slant)
            {



                // Local iconic variables 

                // Local control variables 

                HTuple hv_OS = null, hv_BufferWindowHandle = new HTuple();
                HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
                HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
                HTuple hv_Scale = new HTuple(), hv_Exception = new HTuple();
                HTuple hv_SubFamily = new HTuple(), hv_Fonts = new HTuple();
                HTuple hv_SystemFonts = new HTuple(), hv_Guess = new HTuple();
                HTuple hv_I = new HTuple(), hv_Index = new HTuple(), hv_AllowedFontSizes = new HTuple();
                HTuple hv_Distances = new HTuple(), hv_Indices = new HTuple();
                HTuple hv_FontSelRegexp = new HTuple(), hv_FontsCourier = new HTuple();
                HTuple hv_Bold_COPY_INP_TMP = hv_Bold.Clone();
                HTuple hv_Font_COPY_INP_TMP = hv_Font.Clone();
                HTuple hv_Size_COPY_INP_TMP = hv_Size.Clone();
                HTuple hv_Slant_COPY_INP_TMP = hv_Slant.Clone();

                // Initialize local and output iconic variables 
                //This procedure sets the text font of the current window with
                //the specified attributes.
                //It is assumed that following fonts are installed on the system:
                //Windows: Courier New, Arial Times New Roman
                //Mac OS X: CourierNewPS, Arial, TimesNewRomanPS
                //Linux: courier, helvetica, times
                //Because fonts are displayed smaller on Linux than on Windows,
                //a scaling factor of 1.25 is used the get comparable results.
                //For Linux, only a limited number of font sizes is supported,
                //to get comparable results, it is recommended to use one of the
                //following sizes: 9, 11, 14, 16, 20, 27
                //(which will be mapped internally on Linux systems to 11, 14, 17, 20, 25, 34)
                //
                //Input parameters:
                //WindowHandle: The graphics window for which the font will be set
                //Size: The font size. If Size=-1, the default of 16 is used.
                //Bold: If set to 'true', a bold font is used
                //Slant: If set to 'true', a slanted font is used
                //
                HOperatorSet.GetSystem("operating_system", out hv_OS);
                // dev_get_preferences(...); only in hdevelop
                // dev_set_preferences(...); only in hdevelop
                if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                    new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
                {
                    hv_Size_COPY_INP_TMP = 16;
                }
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    //Set font on Windows systems
                    try
                    {
                        //Check, if font scaling is switched on
                        //open_window(...);
                        HOperatorSet.SetFont(hv_ExpDefaultWinHandle, "-Consolas-16-*-0-*-*-1-");
                        HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, "test_string", out hv_Ascent,
                            out hv_Descent, out hv_Width, out hv_Height);
                        //Expected width is 110
                        hv_Scale = 110.0 / hv_Width;
                        hv_Size_COPY_INP_TMP = ((hv_Size_COPY_INP_TMP * hv_Scale)).TupleInt();
                        //close_window(...);
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        //throw (Exception)
                    }
                    if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))).TupleOr(
                        new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Courier New";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Consolas";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Arial";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Times New Roman";
                    }
                    if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = 1;
                    }
                    else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = 0;
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Bold";
                        throw new HalconException(hv_Exception);
                    }
                    if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_Slant_COPY_INP_TMP = 1;
                    }
                    else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Slant_COPY_INP_TMP = 0;
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Slant";
                        throw new HalconException(hv_Exception);
                    }
                    try
                    {
                        HOperatorSet.SetFont(hv_ExpDefaultWinHandle, ((((((("-" + hv_Font_COPY_INP_TMP) + "-") + hv_Size_COPY_INP_TMP) + "-*-") + hv_Slant_COPY_INP_TMP) + "-*-*-") + hv_Bold_COPY_INP_TMP) + "-");
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        //throw (Exception)
                    }
                }
                else if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Dar"))) != 0)
                {
                    //Set font on Mac OS X systems. Since OS X does not have a strict naming
                    //scheme for font attributes, we use tables to determine the correct font
                    //name.
                    hv_SubFamily = 0;
                    if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_SubFamily = hv_SubFamily.TupleBor(1);
                    }
                    else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleNotEqual("false"))) != 0)
                    {
                        hv_Exception = "Wrong value of control parameter Slant";
                        throw new HalconException(hv_Exception);
                    }
                    if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_SubFamily = hv_SubFamily.TupleBor(2);
                    }
                    else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleNotEqual("false"))) != 0)
                    {
                        hv_Exception = "Wrong value of control parameter Bold";
                        throw new HalconException(hv_Exception);
                    }
                    if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                    {
                        hv_Fonts = new HTuple();
                        hv_Fonts[0] = "Menlo-Regular";
                        hv_Fonts[1] = "Menlo-Italic";
                        hv_Fonts[2] = "Menlo-Bold";
                        hv_Fonts[3] = "Menlo-BoldItalic";
                    }
                    else if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))).TupleOr(
                        new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                    {
                        hv_Fonts = new HTuple();
                        hv_Fonts[0] = "CourierNewPSMT";
                        hv_Fonts[1] = "CourierNewPS-ItalicMT";
                        hv_Fonts[2] = "CourierNewPS-BoldMT";
                        hv_Fonts[3] = "CourierNewPS-BoldItalicMT";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                    {
                        hv_Fonts = new HTuple();
                        hv_Fonts[0] = "ArialMT";
                        hv_Fonts[1] = "Arial-ItalicMT";
                        hv_Fonts[2] = "Arial-BoldMT";
                        hv_Fonts[3] = "Arial-BoldItalicMT";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                    {
                        hv_Fonts = new HTuple();
                        hv_Fonts[0] = "TimesNewRomanPSMT";
                        hv_Fonts[1] = "TimesNewRomanPS-ItalicMT";
                        hv_Fonts[2] = "TimesNewRomanPS-BoldMT";
                        hv_Fonts[3] = "TimesNewRomanPS-BoldItalicMT";
                    }
                    else
                    {
                        //Attempt to figure out which of the fonts installed on the system
                        //the user could have meant.
                        HOperatorSet.QueryFont(hv_ExpDefaultWinHandle, out hv_SystemFonts);
                        hv_Fonts = new HTuple();
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Guess = new HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Regular");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "MT");
                        for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HTuple();
                                hv_Fonts[0] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                        //Guess name of slanted font
                        hv_Guess = new HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Italic");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-ItalicMT");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Oblique");
                        for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HTuple();
                                hv_Fonts[1] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                        //Guess name of bold font
                        hv_Guess = new HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Bold");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldMT");
                        for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HTuple();
                                hv_Fonts[2] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                        //Guess name of bold slanted font
                        hv_Guess = new HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldItalic");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldItalicMT");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldOblique");
                        for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HTuple();
                                hv_Fonts[3] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                    }
                    hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(hv_SubFamily);
                    try
                    {
                        HOperatorSet.SetFont(hv_ExpDefaultWinHandle, (hv_Font_COPY_INP_TMP + "-") + hv_Size_COPY_INP_TMP);
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        //throw (Exception)
                    }
                }
                else
                {
                    //Set font for UNIX systems
                    hv_Size_COPY_INP_TMP = hv_Size_COPY_INP_TMP * 1.25;
                    hv_AllowedFontSizes = new HTuple();
                    hv_AllowedFontSizes[0] = 11;
                    hv_AllowedFontSizes[1] = 14;
                    hv_AllowedFontSizes[2] = 17;
                    hv_AllowedFontSizes[3] = 20;
                    hv_AllowedFontSizes[4] = 25;
                    hv_AllowedFontSizes[5] = 34;
                    if ((int)(new HTuple(((hv_AllowedFontSizes.TupleFind(hv_Size_COPY_INP_TMP))).TupleEqual(
                        -1))) != 0)
                    {
                        hv_Distances = ((hv_AllowedFontSizes - hv_Size_COPY_INP_TMP)).TupleAbs();
                        HOperatorSet.TupleSortIndex(hv_Distances, out hv_Indices);
                        hv_Size_COPY_INP_TMP = hv_AllowedFontSizes.TupleSelect(hv_Indices.TupleSelect(
                            0));
                    }
                    if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))).TupleOr(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(
                        "Courier")))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "courier";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "helvetica";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "times";
                    }
                    if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = "bold";
                    }
                    else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = "medium";
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Bold";
                        throw new HalconException(hv_Exception);
                    }
                    if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("times"))) != 0)
                        {
                            hv_Slant_COPY_INP_TMP = "i";
                        }
                        else
                        {
                            hv_Slant_COPY_INP_TMP = "o";
                        }
                    }
                    else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Slant_COPY_INP_TMP = "r";
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Slant";
                        throw new HalconException(hv_Exception);
                    }
                    try
                    {
                        HOperatorSet.SetFont(hv_ExpDefaultWinHandle, ((((((("-adobe-" + hv_Font_COPY_INP_TMP) + "-") + hv_Bold_COPY_INP_TMP) + "-") + hv_Slant_COPY_INP_TMP) + "-normal-*-") + hv_Size_COPY_INP_TMP) + "-*-*-*-*-*-*-*");
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        if ((int)((new HTuple(((hv_OS.TupleSubstr(0, 4))).TupleEqual("Linux"))).TupleAnd(
                            new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                        {
                            HOperatorSet.QueryFont(hv_ExpDefaultWinHandle, out hv_Fonts);
                            hv_FontSelRegexp = (("^-[^-]*-[^-]*[Cc]ourier[^-]*-" + hv_Bold_COPY_INP_TMP) + "-") + hv_Slant_COPY_INP_TMP;
                            hv_FontsCourier = ((hv_Fonts.TupleRegexpSelect(hv_FontSelRegexp))).TupleRegexpMatch(
                                hv_FontSelRegexp);
                            if ((int)(new HTuple((new HTuple(hv_FontsCourier.TupleLength())).TupleEqual(
                                0))) != 0)
                            {
                                hv_Exception = "Wrong font name";
                                //throw (Exception)
                            }
                            else
                            {
                                try
                                {
                                    HOperatorSet.SetFont(hv_ExpDefaultWinHandle, (((hv_FontsCourier.TupleSelect(
                                        0)) + "-normal-*-") + hv_Size_COPY_INP_TMP) + "-*-*-*-*-*-*-*");
                                }
                                // catch (Exception) 
                                catch (HalconException HDevExpDefaultException2)
                                {
                                    HDevExpDefaultException2.ToHTuple(out hv_Exception);
                                    //throw (Exception)
                                }
                            }
                        }
                        //throw (Exception)
                    }
                }
                // dev_set_preferences(...); only in hdevelop

                return;
            }

            // Chapter: Develop
            // Short Description: Switch dev_update_pc, dev_update_var and dev_update_window to 'off'. 
            public void dev_update_off()
            {

                // Initialize local and output iconic variables 
                //This procedure sets different update settings to 'off'.
                //This is useful to get the best performance and reduce overhead.
                //
                // dev_update_pc(...); only in hdevelop
                // dev_update_var(...); only in hdevelop
                // dev_update_window(...); only in hdevelop

                return;
            }

            // Main procedure 
            private void action()
            {


                // Local iconic variables 

                HObject ho_Bond, ho_Bright = null, ho_Die = null;
                HObject ho_DieGrey = null, ho_Wires = null, ho_WiresFilled = null;
                HObject ho_Balls = null, ho_SingleBalls = null, ho_Rect = null;
                HObject ho_IntermediateBalls = null, ho_Forbidden = null, ho_RegionExpand = null;
                HObject ho_RoundBalls = null, ho_FinalBalls = null;

                // Local control variables 

                HTuple hv_ImageNames = null, hv_Width = null;
                HTuple hv_Height = null, hv_WindowHandle = new HTuple();
                HTuple hv_NumImages = null, hv_I = null, hv_Min = new HTuple();
                HTuple hv_Max = new HTuple(), hv_Range = new HTuple();
                HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
                HTuple hv_Radius = new HTuple(), hv_NumBalls = new HTuple();
                HTuple hv_Diameter = new HTuple(), hv_meanDiameter = new HTuple();
                HTuple hv_mimDiameter = new HTuple();
                // Initialize local and output iconic variables 
                HOperatorSet.GenEmptyObj(out ho_Bond);
                HOperatorSet.GenEmptyObj(out ho_Bright);
                HOperatorSet.GenEmptyObj(out ho_Die);
                HOperatorSet.GenEmptyObj(out ho_DieGrey);
                HOperatorSet.GenEmptyObj(out ho_Wires);
                HOperatorSet.GenEmptyObj(out ho_WiresFilled);
                HOperatorSet.GenEmptyObj(out ho_Balls);
                HOperatorSet.GenEmptyObj(out ho_SingleBalls);
                HOperatorSet.GenEmptyObj(out ho_Rect);
                HOperatorSet.GenEmptyObj(out ho_IntermediateBalls);
                HOperatorSet.GenEmptyObj(out ho_Forbidden);
                HOperatorSet.GenEmptyObj(out ho_RegionExpand);
                HOperatorSet.GenEmptyObj(out ho_RoundBalls);
                HOperatorSet.GenEmptyObj(out ho_FinalBalls);
                try
                {
                    //ball_seq.hdev: Inspection of Ball Bonding
                    //
                    dev_update_off();
                    hv_ImageNames = new HTuple("die/") + (((new HTuple("die_02")).TupleConcat("die_03")).TupleConcat(
                        "die_04")).TupleConcat("die_07");
                    HOperatorSet.SetColored(hv_ExpDefaultWinHandle, 12);
                    ho_Bond.Dispose();
                    HOperatorSet.ReadImage(out ho_Bond, hv_ImageNames.TupleSelect(0));
                    HOperatorSet.GetImageSize(ho_Bond, out hv_Width, out hv_Height);
                    //dev_close_window(...);
                    //dev_open_window(...);
                    set_display_font(hv_ExpDefaultWinHandle, 16, "mono", "true", "false");
                    HOperatorSet.SetDraw(hv_ExpDefaultWinHandle, "margin");
                    HOperatorSet.SetLineWidth(hv_ExpDefaultWinHandle, 3);
                    hv_NumImages = new HTuple(hv_ImageNames.TupleLength());
                    HTuple end_val13 = hv_NumImages - 1;
                    HTuple step_val13 = 1;
                    for (hv_I = 0; hv_I.Continue(end_val13, step_val13); hv_I = hv_I.TupleAdd(step_val13))
                    {
                        ho_Bond.Dispose();
                        HOperatorSet.ReadImage(out ho_Bond, hv_ImageNames.TupleSelect(hv_I));
                        HOperatorSet.DispObj(ho_Bond, hv_ExpDefaultWinHandle);
                        HOperatorSet.MinMaxGray(ho_Bond, ho_Bond, 0, out hv_Min, out hv_Max, out hv_Range);
                        ho_Bright.Dispose();
                        HOperatorSet.Threshold(ho_Bond, out ho_Bright, hv_Max - 80, 255);
                        ho_Die.Dispose();
                        HOperatorSet.ShapeTrans(ho_Bright, out ho_Die, "rectangle2");
                        HOperatorSet.DispObj(ho_Die, hv_ExpDefaultWinHandle);
                        ho_DieGrey.Dispose();
                        HOperatorSet.ReduceDomain(ho_Bond, ho_Die, out ho_DieGrey);
                        HOperatorSet.MinMaxGray(ho_Die, ho_Bond, 0, out hv_Min, out hv_Max, out hv_Range);
                        ho_Wires.Dispose();
                        HOperatorSet.Threshold(ho_DieGrey, out ho_Wires, 0, hv_Min + 30);
                        ho_WiresFilled.Dispose();
                        HOperatorSet.FillUpShape(ho_Wires, out ho_WiresFilled, "area", 1, 100);
                        ho_Balls.Dispose();
                        HOperatorSet.OpeningCircle(ho_WiresFilled, out ho_Balls, 9.5);
                        ho_SingleBalls.Dispose();
                        HOperatorSet.Connection(ho_Balls, out ho_SingleBalls);
                        ho_Rect.Dispose();
                        HOperatorSet.SelectShapeStd(ho_SingleBalls, out ho_Rect, "rectangle1", 90);
                        ho_IntermediateBalls.Dispose();
                        HOperatorSet.Difference(ho_SingleBalls, ho_Rect, out ho_IntermediateBalls
                            );
                        ho_Forbidden.Dispose();
                        HOperatorSet.GenEmptyRegion(out ho_Forbidden);
                        ho_RegionExpand.Dispose();
                        HOperatorSet.ExpandGray(ho_IntermediateBalls, ho_Bond, ho_Forbidden, out ho_RegionExpand,
                            4, "image", 6);
                        ho_RoundBalls.Dispose();
                        HOperatorSet.OpeningCircle(ho_RegionExpand, out ho_RoundBalls, 15.5);
                        ho_FinalBalls.Dispose();
                        HOperatorSet.SortRegion(ho_RoundBalls, out ho_FinalBalls, "first_point",
                            "true", "column");
                        HOperatorSet.SmallestCircle(ho_FinalBalls, out hv_Row, out hv_Column, out hv_Radius);
                        hv_NumBalls = new HTuple(hv_Radius.TupleLength());
                        hv_Diameter = 2 * hv_Radius;
                        hv_meanDiameter = (hv_Diameter.TupleSum()) / hv_NumBalls;
                        hv_mimDiameter = hv_Diameter.TupleMin();
                        HOperatorSet.DispObj(ho_RoundBalls, hv_ExpDefaultWinHandle);
                        if ((int)(new HTuple(hv_I.TupleNotEqual(hv_NumImages))) != 0)
                        {
                            disp_continue_message(hv_ExpDefaultWinHandle, "black", "true");
                        }
                        HDevelopStop();
                    }
                }
                catch (HalconException HDevExpDefaultException)
                {
                    ho_Bond.Dispose();
                    ho_Bright.Dispose();
                    ho_Die.Dispose();
                    ho_DieGrey.Dispose();
                    ho_Wires.Dispose();
                    ho_WiresFilled.Dispose();
                    ho_Balls.Dispose();
                    ho_SingleBalls.Dispose();
                    ho_Rect.Dispose();
                    ho_IntermediateBalls.Dispose();
                    ho_Forbidden.Dispose();
                    ho_RegionExpand.Dispose();
                    ho_RoundBalls.Dispose();
                    ho_FinalBalls.Dispose();

                    throw HDevExpDefaultException;
                }
                ho_Bond.Dispose();
                ho_Bright.Dispose();
                ho_Die.Dispose();
                ho_DieGrey.Dispose();
                ho_Wires.Dispose();
                ho_WiresFilled.Dispose();
                ho_Balls.Dispose();
                ho_SingleBalls.Dispose();
                ho_Rect.Dispose();
                ho_IntermediateBalls.Dispose();
                ho_Forbidden.Dispose();
                ho_RegionExpand.Dispose();
                ho_RoundBalls.Dispose();
                ho_FinalBalls.Dispose();

            }

            public void InitHalcon()
            {
                // Default settings used in HDevelop 
                HOperatorSet.SetSystem("width", 512);
                HOperatorSet.SetSystem("height", 512);
            }

            public void RunHalcon(HTuple Window)
            {
                hv_ExpDefaultWinHandle = Window;
                action();
            }

        }


    }
}
