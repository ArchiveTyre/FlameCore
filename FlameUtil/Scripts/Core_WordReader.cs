using UnityEngine;
using System;

/*
WordReader.java

Copyright (C) 2016 Henrik Björkman (www.eit.se/hb)
License: www.eit.se/rsb/license


History:
2013-02-27
Created by Henrik Bjorkman (www.eit.se/hb)

2016-01-01
Cleanup by Henrik Bjorkman (www.eit.se/hb)

2016-02-08
Converted to C# by Alexander Björkman (www.eit.se/alexander)

*/






public class Core_WordReader {


     //int tcpConnectionTimeoutMs=0;

     String inputStr=null;
     int inputStrOffset=0;
     int inputStrLength=0;

     public static void debug(string str)
     {
         //WordWriter.safeDebug("WordReader: "+str);
		 Debug.Log ("WordReader: "+str);
     }

     /*public WordReader(ClientConnection clientConnection)
     {
         this.clientConnection = clientConnection;
     }*/


     // This constructor is to be used by extending classes, it would make little sense to create a WordReader without something to read.
     public Core_WordReader()
     {
     }


     // This is used to read words etc from a string.
     public Core_WordReader(String str)
     {
         inputStr=str;
         inputStrOffset=0;
         inputStrLength = str.Length;
         //debug("WordReader \"" + inputStr + "\"");
     }




     protected void checkStrBuffer()
     {
         if (inputStrOffset>=inputStrLength)
         {
             // All chartacters in current inputStr has been processed, so we can forget that buffer.
             inputStr=null;
         }

         if (inputStr==null)
         {
             inputStrOffset = 0;
             inputStrLength = 0;
         }

     }

     public char getWaitChar()
     {
         checkStrBuffer();

         if (inputStr==null)
         {
             return char.MinValue;
         }

         if (inputStrLength==0)
         {
             debug("getWaitChar got empty string");
             return '\n';
         }

         char ch = inputStr[inputStrOffset++];
         return ch;
     }

     /*
     public char getWaitChar()
     {
         checkStrBuffer();


         while (inputStrLength==0)
         {
             if (inputStr==null)
             {
                 return char.MinValue;
             }

             checkStrBuffer();
         }

         const char ch=inputStr[inputStrOffset++);
         return ch;
     }
     */

     /*
     public static bool isSeparator(char ch, char separator) {
         return (ch==separator) || Char.IsWhiteSpace(ch);
     }
     */

     public static bool isCharInStr(char ch, string separator)
     {
         for (int i=0; i<separator.Length;i++)
         {
             if (ch==separator[i])
             {
                 return true;
             }
         }
         return false;
     }


     // Returns true is ch is a separator between words
     public static bool isSeparator(char ch, string separator)
     {
         if (Char.IsWhiteSpace(ch))
         {
             return true;
         }
         else if (ch == char.MinValue)
         {
             return true;
         }
         else if (isLetterOrDigitOrUnderscore(ch))
         {
             return false;
         }
         else if (isCharInStr(ch, separator))
         {
             return true;
         }
         return false;
     }


     // To get one word from the input, words are separated by separator
     // deprecated, use readToken
     public string readWord(string separator)
     {
         System.Text.StringBuilder sb=new System.Text.StringBuilder();
         int state=0;

         while (state<3)
         {
             char ch=getWaitChar();

             // Check for end of file or closed socket
             if (ch==char.MinValue)
             {
                 break;
             }

             switch(state)
             {
                  case 0: // initial state, skipping spaces and separators
                  {
                      // skip spaces, if this was not a separator add it to the buffer and change state.
                      if (!isSeparator(ch, separator))
                      {
                          sb.Append(ch);
                          state=1;
                      }
                      break;
                  }
                  case 1: // a normal word
                  {
                     // Now look for the trailing space
                      if (isSeparator(ch, separator))
                      {
                          state=4;
                      }
                     else
                     {
                         sb.Append(ch);
                     }
                     break;
                  }
                  default:
                  {
                      break;
                  }
             }
         }

         return sb.ToString();
     }

     // deprecated, use readToken
     public string readWord(char separator)
     {
         return readWord(""+separator);
     }

     // deprecated, use readToken
     public string readWord()
     {
         return readWord("");
     }

     public string readName()
     {
         return readWord('.'); // Instead of period '.' we should perhaps have use slash '/' here.
     }



     public void skipWhiteAndCheckStrBuffer()
     {
         // skip trailing spaces after previous word
         while ((inputStrOffset<inputStrLength) && (isSeparator(inputStr[inputStrOffset], "")))
         {
             inputStrOffset++;
         }

         checkStrBuffer();

         // skip leading space
         while ((inputStrOffset<inputStrLength) && 
(isSeparator(inputStr[inputStrOffset], "")))
         {
             inputStrOffset++;
         }
     }

     // true if next thing to read is a string
     public bool isNextString()
     {
         skipWhiteAndCheckStrBuffer();

         if (inputStr.Length<=inputStrOffset)
         {
             return false;
         }

         char ch=inputStr[inputStrOffset];

         return ch=='"';
     }


     public bool isNextBegin()
     {
         skipWhiteAndCheckStrBuffer();

         if (inputStr.Length<1)
         {
             return false;
         }

         char ch=inputStr[inputStrOffset];

         return ch=='{';
     }

     public bool isNextEnd()
     {
         skipWhiteAndCheckStrBuffer();

         if (inputStr.Length<1)
         {
             return false;
         }

         char ch=inputStr[inputStrOffset];

         return ch=='}';
     }

     // true if the string looks like it can be an int (or other number).
     public static bool isInt(string str)
     {
         if (str.Length==0)
         {
             return false;
         }
         char ch=str[0];
         return (ch>='0' && ch<='9') || ch=='-' || ch=='+' ;
     }



     // true if next thing to read looks like its a number, an int or a float.
     public bool isNextIntOrFloat()
     {
         bool found=false;
         try
         {
             if (inputStr==null)
             {
                 return false;
             }

             skipWhiteAndCheckStrBuffer();

             int n=inputStr.Length;

             if (n<1)
             {
                 return false;
             }

             int i=inputStrOffset;

             char ch=inputStr[i];
             i++;
             if ((ch>='0' && ch<='9'))
             {
                 // OK, first char is a digit.
                 found=true;
             }
             else if (ch=='-' || ch=='+')
             {
                 // Ok if digits follow
                 if (n<=1)
                 {
                     // No the string ended after +/-, not an int
                     return false;
                 }
             }
             else
             {
                 // Not a number. First char is a letter or space or something.
                 return false;
             }


             while(i<n)
             {
                 ch=inputStr[i];
                 i++;
                 if ((ch>='0' && ch<='9'))
                 {
                     found=true;
                     // OK
                 }
                 else if (((ch=='.') || (ch=='-') || (ch=='E')) && (found))
                 {
                     // OK, will accept parts of a float also.
                     // Floats can look like this: 9.223372E18
                     // This code does not check that there is only one E and one '.'.
                 }
                 else if (isWhiteSpaceCrLf(ch))
                 {
                     // String ends here, if it was numbers this far then it is a number.
                     return found;
                 }
                 else
                 {
                     // Not a number.
                     return false;
                 }
             }
         }
         catch
         {
             return false;
         }

         return found;
     }



     // true if next thing to read is a string that begins with same as str
     public bool isNext(string str1)
     {
         skipWhiteAndCheckStrBuffer();

         if (inputStr.Length<str1.Length+inputStrOffset)
         {
             return false;
         }

         int n=str1.Length;

         int i=0;


         while(i<n)
         {
             char ch1=str1[i];
             char ch2=inputStr[inputStrOffset+i];

             if (ch1!=ch2)
             {
                 return false;
             }
             i++;
         }

         // TODO We might want to check that a word in input string ended here.

         return true;
     }


     // Read a string, a string is a number of words surrounded by quotes '"'.
     public string readString()
     {
         string tmp = readToken(",;:/[");
         return tmp;
     }

     // Reads a small number -128 to 127 that is written in ascii. This does not read a binary byte from the input stream.
     public byte readByte()
     {
         string str=readToken(",;:/[");
         try {
             int i = Int32.Parse(str);
             return (byte)i;
         }
         catch
         {
			//WordWriter.safeError("\nWordReader.readInt: NumberFormatException expected byte but found " + str + " ");
			throw(new FormatException("expected byte but found '" + str + "'"));
         }
     }

     // Reads a number that is written in ascii.
     // The integer numbers are usually separated by space. But they can also be separated by ',;:/'
     /*public int readInt()
     {
         String str=readWord(",;:/[");
         try {
             int i = Integer.parseInt(str);
             return i;
         }
         catch (NumberFormatException e)
         {
             WordWriter.safeError("\nWordReader.readInt: 
NumberFormatException expected int but found '" + str + "' ");
             e.printStackTrace();
         }
         return 0;
     }*/
     public int readInt()
     {
         string str=readToken(",;:/[");
         try {
             int i = Int32.Parse(str);
             return i;
         }
         catch (FormatException e)
         {
			//WordWriter.safeError("WordReader.readInt: NumberFormatException expected int but found '" + str + "' ");
            
			 Debug.LogError (e); 
			 throw(new FormatException ("expected int but found '" + str + "' "));
			
         }
     }

     // Reads a number that is written in ascii.
     // The long numbers are usually separated by space. But they can also be separated by ',;:/'
     public long readLong()
     {
         String str=readToken(",;:/[");
         try {
             long i = Int64.Parse(str);
             return i;
         }
         catch
         {
 			//WordWriter.safeError("\nWordReader.readInt: NumberFormatException expected long but found " + str + " ");
             throw(new FormatException("expected long but found '" + str + "' "));
         }
     }

     public float readFloat()
     {
         String str=readToken(",;:/[");
         try {
             float f = Int64.Parse(str);
             return f;
         }
         catch
         {
			//WordWriter.safeError("\nWordReader.readFloat: NumberFormatException expected float but found " + str + " ");
             throw(new FormatException("expected float but found '" + str + "' "));
         }
     }

     /*
     // read an entire line of input, until a '\n' is found.
     public String readLine()
     {
         //debug("readLine");
         checkStrBuffer();
         if (inputStr!=null) {
             int n = inputStrOffset;
             inputStrOffset=inputStrLength;
             String str=inputStr.Substring(n,inputStrOffset);
             inputStr=null;
             //debug("line \""+getLineWithoutLf(str)+"\"");
             return str;
         }
         debug("line null");
         return null;
     }
*/

     // read an entire line of input, until a '\n' is found.
     // skip leading spaces
     public String readLine()
     {
         System.Text.StringBuilder sb=new System.Text.StringBuilder();
         int state=0;

         while (state<3)
         {
             char ch=getWaitChar();

             // Check for end of file or closed socket
             if (ch==char.MinValue)
             {
                 break;
             }

             switch(state)
             {
                  case 0:
                  {
                      // skip spaces
                      if (!isSeparator(ch, ""))
                      {
                          sb.Append(ch);
                          state=1;
                      }
                      break;
                  }
                  case 1:
                  {
                     // Now look for the trailing '\n'
                      if (ch=='\n')
                      {
                          state=4;
                      }
                     else
                     {
                         sb.Append(ch);
                     }
                     break;
                  }
                  default:
                  {
                      break;
                  }
             }
             //debug("readLine: "+sb.ToString());
         }

         return sb.ToString();
     }


     // If we are reading a string then this is: "isNotAtEnd".
     // If it is a tcp/ip connection then this is: "isOpen"
     public  bool isOpenAndNotEnd()
     {
         if ((inputStr!=null) && (inputStrOffset!=inputStrLength))
         {
             return true;
         }
         return false;
     }


     public  void close()
     {

         inputStr=null;
         inputStrOffset=0;
         inputStrLength=0;
     }

     protected static String skipLeadingSpace(String str) {
         int i=0;
		 System.Text.StringBuilder someString = new System.Text.StringBuilder("someString");

         while (someString[i]==' ')
         {
             i++;
         }

         return someString.ToString().Substring(i);
     }


     // This gives a string with the first word from str, words are separated by white space.
     public static String getWord(String str) {

         if (str==null)
         {
             return null;
         }

         int b=0;

         // skip leading space
         while ((b<str.Length && (Char.IsWhiteSpace(str[b]))))
         {
             b++;
         }

         int e=b;

         // find the end of the word
         while ((e<str.Length) && (!Char.IsWhiteSpace(str[e])))
         {
             e++;
         }

         return str.Substring(b, e);
     }

     // This gives a string with the first word from str, words are separated by white space.
     public static String getWord(String str, String separator) {

         if (str==null)
         {
             return null;
         }

         int b=0;

         // skip leading space
         while ((b<str.Length && (isSeparator(str[b], separator))))
         {
             b++;
         }

         int e=b;

         // find the end of the word
         while ((e<str.Length && (!isSeparator(str[e], separator))))
         {
             e++;
         }

         return str.Substring(b, e);
     }


     // This gives a string without the first word in str, to be used in combination with getWord
     public static String getRemainingLine(String str) {

         if (str==null)
         {
             return null;
         }

         int b=0;

         // skip leading space
         while ((b<str.Length && (Char.IsWhiteSpace(str[b]))))
         {
             b++;
         }

         int e=b;

         // find the end of the word
         while ((e<str.Length && (!Char.IsWhiteSpace(str[e]))))
         {
             e++;
         }

         // skip leading space of second word
         while ((e<str.Length && (Char.IsWhiteSpace(str[e]))))
         {
             e++;
         }

         return str.Substring(e);
     }


     // This gives a string without the first word in str, to be used in combination with getWord
     public static String getRemainingLine(String str, String separator) {

         if (str==null)
         {
             return null;
         }

         int b=0;

         // skip leading space
         while ((b<str.Length && (isSeparator(str[b], separator))))
         {
             b++;
         }

         int e=b;

         // find the end of the word
         while ((e<str.Length && (!isSeparator(str[e], separator))))
         {
             e++;
         }

         // skip leading space of second word
         while ((e<str.Length) && (isSeparator(str[e], separator)))
         {
             e++;
         }

         return str.Substring(e);
     }


     public static bool isWhiteSpaceCrLf(char ch)
     {
         return Char.IsWhiteSpace(ch) || (ch=='\r') || (ch=='\n');
     }

     public static bool isLetterOrDigitOrUnderscore(char ch)
     {
         return (Char.IsLetterOrDigit(ch) || (ch=='_'));
     }



     // Get the line without leading spaces and trailing line feeds.
     public static String getLineWithoutLf(String str) {

         if (str==null)
         {
             return null;
         }

         int b=0;

         // skip leading space
         while ((b<str.Length) && (Char.IsWhiteSpace(str[b])))
         {
             b++;
         }

         int e=str.Length-1;

         // skip trailing space
         while ((e>b) && ( isWhiteSpaceCrLf(str[e])))
         {
             e--;
         }

         return str.Substring(b,e+1);
     }


     // Replace all a characters with b characters in a string
     public static string replaceCharacters(String str, char a, char b)
     {
         System.Text.StringBuilder sb=new System.Text.StringBuilder();
         for(int i=0;i<str.Length;i++)
         {
             char ch=str[i];
             if (ch==a)
             {
                 sb.Append(b);
             }
             else
             {
                 sb.Append(ch);
             }
         }
         return sb.ToString();
     }



     public static String getLastWord(String str, char sep)
     {
         str=getLineWithoutLf(str);

         int e=str.Length-1;
         int b=e;

         // skip trailing space
         while ((b>0) && (!isWhiteSpaceCrLf(str[b-1])) && (str[b-1]!=sep))
         {
             b--;
         }

         return str.Substring(b,e+1);
     }


     public static String removeQuotes(String str)
     {
         int len=str.Length;
         if (len>=2)
         {
             if ((str[0]=='"') && (str[len-1]=='"'))
             {
                 return str.Substring(1,len-1);
             }
         }
         return str;
     }

     public static int genNumberOfWords(String str)
     {
         int n=0;
         Core_WordReader wr=new Core_WordReader(str);
         while(wr.isOpenAndNotEnd())
         {
             wr.readString();
             ++n;
         }
         return n;
     }

     /*public static string[] split(String str)
     {
         int n=genNumberOfWords(str);
         string a;
         int i=0;
         WordReader wr=new WordReader(str);
         while(wr.isOpenAndNotEnd())
         {
             a[i]=wr.readString();
             i++;
         }
         return a;
     }*/


     // Reads a bool that is written in ascii.
     public bool readbool()
     {
         String str=readToken(",;:/[");
         try {
             int i = Int32.Parse(str);
             return (i==0)?false:true;
         }
         catch
         {
             throw(new FormatException("expected long but found '" + str + "' "));
         }

     }



     public char previewChar()
     {
         checkStrBuffer();

         if (inputStr==null)
         {
             return char.MinValue;
         }

         if (inputStrLength==0)
         {
             debug("getWaitChar got empty string");
             return '\n';
         }

         char ch=inputStr[inputStrOffset];
         return ch;
     }


     public static bool isInternalPartOfNumber(char ch)
     {
         if ((ch>='0' && ch<='9'))
         {
             return true;
         }
         else if (((ch=='.') || (ch=='-') || (ch=='E')))
         {
             // OK, will accept parts of a float also.
             // Floats can look like this: 9.223372E18
             // This code does not check that there is only one E and one '.'.
             return true;
         }
         return false;
     }

     public enum ReadTokenState {
         InitialState,
         ParsingNumber,
         ParsingName,
         NumberOrOpcode,
         ParsingOpcode,
         InsideString,
         EscapeChar,
         EndState
     }




     // To get one word from the input
     public string readToken(String separatorChars)
     {
         const String singleCharTokens=",.;[](){}";
         const String multiCharTokens="~^&|<>:=!*/%?";
         System.Text.StringBuilder sb=new System.Text.StringBuilder();
         ReadTokenState state=ReadTokenState.InitialState;
         char quoteChar= Convert.ToChar(0);


         while (state!=ReadTokenState.EndState)
         {
             char ch=previewChar();

             // Check for end of file or closed socket
             if (ch==char.MinValue)
             {
                 break;
             }

             switch(state)
             {
                 case ReadTokenState.InitialState: // initial state, skipping spaces and separators
                 {
                     // if this is not a separator add it to the buffer and change state.
                     if (isSeparator(ch, separatorChars))
                     {
                         // skip leading spaces
                         getWaitChar();
                     }
                     else if (Char.IsDigit(ch))
                     {
                         sb.Append(getWaitChar());
                         state=ReadTokenState.ParsingNumber;
                     }
                     else if (isLetterOrDigitOrUnderscore(ch))
                      {
                         sb.Append(getWaitChar());
                         state=ReadTokenState.ParsingName;
                      }
                     else if (ch=='-')
                     {
                         sb.Append(getWaitChar());
                         state=ReadTokenState.NumberOrOpcode;
                     }
                     else if (ch=='"')
                     {
                         quoteChar=getWaitChar();
                         state=ReadTokenState.InsideString;
                     }
                     else if (ch=='\'')
                     {
                         // single quoted string
                         quoteChar=getWaitChar();
                         state=ReadTokenState.InsideString;
                     }
                     else if (isCharInStr(ch, singleCharTokens))
                     {
                         sb.Append(getWaitChar());
                          state=ReadTokenState.EndState;
                     }
                     else if (isCharInStr(ch, multiCharTokens))
                     {
                         sb.Append(getWaitChar());
                          state=ReadTokenState.ParsingOpcode;
                     }
                     else
                     {
                         // what to do with this one?
                         sb.Append(getWaitChar());
                         state=ReadTokenState.EndState;
                     }
                     break;
                 }
                 case ReadTokenState.ParsingNumber: // a number,
                 {
                     // Now look for the trailing space
                      if (isInternalPartOfNumber(ch))
                      {
                         sb.Append(getWaitChar());
                      }
                     else
                     {
                          state=ReadTokenState.EndState;
                     }
                     break;
                 }
                 case ReadTokenState.ParsingName: // a normal word or name,
                 {
                     // Now look for the trailing space
                      if (isLetterOrDigitOrUnderscore(ch))
                      {
                         sb.Append(getWaitChar());
                      }
                     else
                     {
                         string r1=sb.ToString();
                         if (r1=="null")
                         {
                             //debug("null");
                             return null;
                         }
                          state=ReadTokenState.EndState;
                     }
                     break;
                 }
                 case ReadTokenState.NumberOrOpcode: // a number or an opcode?
                 {
                      if (Char.IsDigit(ch))
                      {
                          // Part of a number, as in -9.01
                         sb.Append(getWaitChar());
                         state=ReadTokenState.ParsingNumber;
                      }
                     else if (isCharInStr(ch, multiCharTokens))
                     {
                         sb.Append(getWaitChar());
                          state=ReadTokenState.ParsingOpcode;
                     }
                     else
                     {
                          state=ReadTokenState.EndState;
                     }
                     break;
                 }
                 case ReadTokenState.ParsingOpcode: // part of an opcode
                 {
                     if (isCharInStr(ch, multiCharTokens))
                     {
                         sb.Append(getWaitChar());
                     }
                     else
                     {
                          state=ReadTokenState.EndState;
                     }
                     break;
                 }

                 case ReadTokenState.InsideString:
                 {
                     // Now look for the trailing '"' or the escape char '\'
                     if (ch==quoteChar)
                     {
                         // This marks the end of the string
                         getWaitChar();
                         state=ReadTokenState.EndState;
                     }
                     else if (ch=='\\')
                     {
                         // This is the escape char, special char follows-
                         getWaitChar();
                         state=ReadTokenState.EscapeChar;
                     }
                     else
                     {
                         sb.Append(getWaitChar());
                     }
                     break;
                 }
                 case ReadTokenState.EscapeChar:
                 {
                     getWaitChar();
                     // https://en.wikipedia.org/wiki/Escape_sequences_in_C
                     switch(ch)
                     {
                         /*case 'a':
                         sb.Append('\a');
                         break;*/
                     case 'b':
                         sb.Append('\b');
                         break;
                     case 'f':
                         sb.Append('\f');
                         break;
                     case 'n':
                         sb.Append('\n');
                         break;
                     case 'r':
                         sb.Append('\r');
                         break;
                     case 't':
                         sb.Append('\t');
                         break;
                     case 'u':
                     case 'U':
                         debug("unicode escape sequence is not supported yet");
                         break;
                     /*case 'v':
                         sb.Append('\v');
                         break;*/
                     case 'x':
                         debug("hex escape sequence is not supported yet");
                         break;
                     case '\\':
                     case '\'':
                     case '\"':
                     case '?':
                         sb.Append(ch);
                         break;
                     default:
                     {
                         if ((ch>='0') && (ch<='9'))
                         {
                             debug("octal escape sequence is not supported yet");
                         }
                         else
                         {
                             debug("incorrect escape sequence");
                             }
                             sb.Append(ch);
                             break;
                         }
                     }

                     state=ReadTokenState.InsideString;
                     break;
                  }

                 default:
                 {
                     break;
                 }
             }
         }

         String r=sb.ToString();
         //debug("'"+r+"'");
         return r;
     }

}