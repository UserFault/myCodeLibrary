
using System;

namespace MyCodeLibrary.TextProcessing.UDE.Core
{   
    public abstract class GreekModel : SequenceModel
    {
        // Model Table: 
        // total sequences: 100%
        // first 512 sequences: 98.2851%
        // first 1024 sequences:1.7001%
        // rest  sequences:     0.0359%
        // negative sequences:  0.0148% 
        private readonly static byte[] GREEK_LANG_MODEL = {
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,3,2,2,3,3,3,3,3,3,3,3,1,3,3,3,0,2,2,3,3,0,3,0,3,2,0,3,3,3,0,
        3,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,3,0,3,3,0,3,2,3,3,0,3,2,3,3,3,0,0,3,0,3,0,3,3,2,0,0,0,
        2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,
        0,2,3,2,2,3,3,3,3,3,3,3,3,0,3,3,3,3,0,2,3,3,0,3,3,3,3,2,3,3,3,0,
        2,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,2,3,3,2,3,3,3,3,3,3,3,3,3,3,3,3,0,2,1,3,3,3,3,2,3,3,2,3,3,2,0,
        0,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,0,3,3,3,3,3,3,0,3,3,0,3,3,3,3,3,3,3,3,3,3,0,3,2,3,3,0,
        2,0,1,0,2,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,
        0,3,3,3,3,3,2,3,0,0,0,0,3,3,0,3,1,3,3,3,0,3,3,0,3,3,3,3,0,0,0,0,
        2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,3,0,3,0,3,3,3,3,3,0,3,2,2,2,3,0,2,3,3,3,3,3,2,3,3,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,3,3,2,2,2,3,3,3,3,0,3,1,3,3,3,3,2,3,3,3,3,3,3,3,2,2,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,3,2,0,3,0,0,0,3,3,2,3,3,3,3,3,0,0,3,2,3,0,2,3,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,0,3,3,3,3,0,0,3,3,0,2,3,0,3,0,3,3,3,0,0,3,0,3,0,2,2,3,3,0,0,
        0,0,1,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,3,2,0,3,2,3,3,3,3,0,3,3,3,3,3,0,3,3,2,3,2,3,3,2,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,2,3,2,3,3,3,3,3,3,0,2,3,2,3,2,2,2,3,2,3,3,2,3,0,2,2,2,3,0,
        2,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,3,0,0,0,3,3,3,2,3,3,0,0,3,0,3,0,0,0,3,2,0,3,0,3,0,0,2,0,2,0,
        0,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,0,3,3,3,3,3,3,0,3,3,0,3,0,0,0,3,3,0,3,3,3,0,0,1,2,3,0,
        3,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,3,2,0,0,3,2,2,3,3,0,3,3,3,3,3,2,1,3,0,3,2,3,3,2,1,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,3,3,0,2,3,3,3,3,3,3,0,0,3,0,3,0,0,0,3,3,0,3,2,3,0,0,3,3,3,0,
        3,0,0,0,2,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,0,3,3,3,3,3,3,0,0,3,0,3,0,0,0,3,2,0,3,2,3,0,0,3,2,3,0,
        2,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,3,1,2,2,3,3,3,3,3,3,0,2,3,0,3,0,0,0,3,3,0,3,0,2,0,0,2,3,1,0,
        2,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,0,3,3,3,3,0,3,0,3,3,2,3,0,3,3,3,3,3,3,0,3,3,3,0,2,3,0,0,3,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,0,3,3,3,0,0,3,0,0,0,3,3,0,3,0,2,3,3,0,0,3,0,3,0,3,3,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,3,0,0,0,3,3,3,3,3,3,0,0,3,0,2,0,0,0,3,3,0,3,0,3,0,0,2,0,2,0,
        0,0,0,0,1,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,3,3,0,3,0,2,0,3,2,0,3,2,3,2,3,0,0,3,2,3,2,3,3,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,3,0,0,2,3,3,3,3,3,0,0,0,3,0,2,1,0,0,3,2,2,2,0,3,0,0,2,2,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,0,3,3,3,2,0,3,0,3,0,3,3,0,2,1,2,3,3,0,0,3,0,3,0,3,3,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,2,3,3,3,0,3,3,3,3,3,3,0,2,3,0,3,0,0,0,2,1,0,2,2,3,0,0,2,2,2,0,
        0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,3,0,0,2,3,3,3,2,3,0,0,1,3,0,2,0,0,0,0,3,0,1,0,2,0,0,1,1,1,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,3,1,0,3,0,0,0,3,2,0,3,2,3,3,3,0,0,3,0,3,2,2,2,1,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,0,3,3,3,0,0,3,0,0,0,0,2,0,2,3,3,2,2,2,2,3,0,2,0,2,2,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,3,3,3,2,0,0,0,0,0,0,2,3,0,2,0,2,3,2,0,0,3,0,3,0,3,1,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,3,2,3,3,2,2,3,0,2,0,3,0,0,0,2,0,0,0,0,1,2,0,2,0,2,0,
        0,2,0,2,0,2,2,0,0,1,0,2,2,2,0,2,2,2,0,2,2,2,0,0,2,0,0,1,0,0,0,0,
        0,2,0,3,3,2,0,0,0,0,0,0,1,3,0,2,0,2,2,2,0,0,2,0,3,0,0,2,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,3,0,2,3,2,0,2,2,0,2,0,2,2,0,2,0,2,2,2,0,0,0,0,0,0,2,3,0,0,0,2,
        0,1,2,0,0,0,0,2,2,0,0,0,2,1,0,2,2,0,0,0,0,0,0,1,0,2,0,0,0,0,0,0,
        0,0,2,1,0,2,3,2,2,3,2,3,2,0,0,3,3,3,0,0,3,2,0,0,0,1,1,0,2,0,2,2,
        0,2,0,2,0,2,2,0,0,2,0,2,2,2,0,2,2,2,2,0,0,2,0,0,0,2,0,1,0,0,0,0,
        0,3,0,3,3,2,2,0,3,0,0,0,2,2,0,2,2,2,1,2,0,0,1,2,2,0,0,3,0,0,0,2,
        0,1,2,0,0,0,1,2,0,0,0,0,0,0,0,2,2,0,1,0,0,2,0,0,0,2,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,2,3,3,2,2,0,0,0,2,0,2,3,3,0,2,0,0,0,0,0,0,2,2,2,0,2,2,0,2,0,2,
        0,2,2,0,0,2,2,2,2,1,0,0,2,2,0,2,0,0,2,0,0,0,0,0,0,2,0,0,0,0,0,0,
        0,2,0,3,2,3,0,0,0,3,0,0,2,2,0,2,0,2,2,2,0,0,2,0,0,0,0,0,0,0,0,2,
        0,0,2,2,0,0,2,2,2,0,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,2,0,0,3,2,0,2,2,2,2,2,0,0,0,2,0,0,0,0,2,0,1,0,0,2,0,1,0,0,0,
        0,2,2,2,0,2,2,0,1,2,0,2,2,2,0,2,2,2,2,1,2,2,0,0,2,0,0,0,0,0,0,0,
        0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,
        0,2,0,2,0,2,2,0,0,0,0,1,2,1,0,0,2,2,0,0,2,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,3,2,3,0,0,2,0,0,0,2,2,0,2,0,0,0,1,0,0,2,0,2,0,2,2,0,0,0,0,
        0,0,2,0,0,0,0,2,2,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,
        0,2,2,3,2,2,0,0,0,0,0,0,1,3,0,2,0,2,2,0,0,0,1,0,2,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,2,0,2,0,3,2,0,2,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
        0,0,2,0,0,0,0,1,1,0,0,2,1,2,0,2,2,0,1,0,0,1,0,0,0,2,0,0,0,0,0,0,
        0,3,0,2,2,2,0,0,2,0,0,0,2,0,0,0,2,3,0,2,0,0,0,0,0,0,2,2,0,0,0,2,
        0,1,2,0,0,0,1,2,2,1,0,0,0,2,0,0,2,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,2,1,2,0,2,2,0,2,0,0,2,0,0,0,0,1,2,1,0,2,1,0,0,0,0,0,0,0,0,0,0,
        0,0,2,0,0,0,3,1,2,2,0,2,0,0,0,0,2,0,0,0,2,0,0,3,0,0,0,0,2,2,2,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,2,1,0,2,0,1,2,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,1,0,0,0,0,0,0,2,
        0,2,2,0,0,2,2,2,2,2,0,1,2,0,0,0,2,2,0,1,0,2,0,0,2,2,0,0,0,0,0,0,
        0,0,0,0,1,0,0,0,0,0,0,0,3,0,0,2,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,2,
        0,1,2,0,0,0,0,2,2,1,0,1,0,1,0,2,2,2,1,0,0,0,0,0,0,1,0,0,0,0,0,0,
        0,2,0,1,2,0,0,0,0,0,0,0,0,0,0,2,0,0,2,2,0,0,0,0,1,0,0,0,0,0,0,2,
        0,2,2,0,0,0,0,2,2,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,2,0,0,2,0,0,0,
        0,2,2,2,2,0,0,0,3,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,2,0,0,0,0,0,0,1,
        0,0,2,0,0,0,0,1,2,0,0,0,0,0,0,2,2,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,
        0,2,0,2,2,2,0,0,2,0,0,0,0,0,0,0,2,2,2,0,0,0,2,0,0,0,0,0,0,0,0,2,
        0,0,1,0,0,0,0,2,1,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,
        0,3,0,2,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,0,2,
        0,0,2,0,0,0,0,2,2,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,2,0,2,2,1,0,0,0,0,0,0,2,0,0,2,0,2,2,2,0,0,0,0,0,0,2,0,0,0,0,2,
        0,0,2,0,0,2,0,2,2,0,0,0,0,2,0,2,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,0,
        0,0,3,0,0,0,2,2,0,2,2,0,0,0,0,0,2,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,2,0,0,0,0,0,
        0,2,2,2,2,2,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,1,
        0,0,0,0,0,0,0,2,1,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,2,2,0,0,0,0,0,2,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,
        0,2,0,0,0,2,0,0,0,0,0,1,0,0,0,0,2,2,0,0,0,1,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,2,0,0,0,
        0,2,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,1,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,2,0,2,0,0,0,
        0,0,0,0,0,0,0,0,2,1,0,0,0,0,0,0,2,0,0,0,1,2,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        };

        public GreekModel(byte[] charToOrderMap, string name) 
            : base(charToOrderMap, GREEK_LANG_MODEL, 0.982851f, false, name)
        {
        }        
    }
    
    public class Latin7Model : GreekModel
    {
        /****************************************************************
        255: Control characters that usually does not exist in any text
        254: Carriage/Return
        253: symbol (punctuation) that does not belong to word
        252: 0 - 9
        *****************************************************************/
        //Character Mapping Table:
        private readonly static byte[] LATIN7_CHAR_TO_ORDER_MAP = {
            255,255,255,255,255,255,255,255,255,255,254,255,255,254,255,255,  //00
            255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,  //10
            +253,253,253,253,253,253,253,253,253,253,253,253,253,253,253,253,  //20
            252,252,252,252,252,252,252,252,252,252,253,253,253,253,253,253,  //30
            253, 82,100,104, 94, 98,101,116,102,111,187,117, 92, 88,113, 85,  //40
             79,118,105, 83, 67,114,119, 95, 99,109,188,253,253,253,253,253,  //50
            253, 72, 70, 80, 81, 60, 96, 93, 89, 68,120, 97, 77, 86, 69, 55,  //60
             78,115, 65, 66, 58, 76,106,103, 87,107,112,253,253,253,253,253,  //70
            255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,  //80
            255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,  //90
            +253,233, 90,253,253,253,253,253,253,253,253,253,253, 74,253,253,  //a0
            253,253,253,253,247,248, 61, 36, 46, 71, 73,253, 54,253,108,123,  //b0
            110, 31, 51, 43, 41, 34, 91, 40, 52, 47, 44, 53, 38, 49, 59, 39,  //c0
             35, 48,250, 37, 33, 45, 56, 50, 84, 57,120,121, 17, 18, 22, 15,  //d0
            124,  1, 29, 20, 21,  3, 32, 13, 25,  5, 11, 16, 10,  6, 30,  4,  //e0
              9,  8, 14,  7,  2, 12, 28, 23, 42, 24, 64, 75, 19, 26, 27,253,  //f0
        };
        
        public Latin7Model() : base(LATIN7_CHAR_TO_ORDER_MAP, Charsets.ISO_8859_7)
        {
        }
    }
    
    public class Win1253Model : GreekModel
    {
        private readonly static byte[] WIN1253__CHAR_TO_ORDER_MAP = {
            255,255,255,255,255,255,255,255,255,255,254,255,255,254,255,255,  //00
            255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,  //10
            +253,253,253,253,253,253,253,253,253,253,253,253,253,253,253,253,  //20
            252,252,252,252,252,252,252,252,252,252,253,253,253,253,253,253,  //30
            253, 82,100,104, 94, 98,101,116,102,111,187,117, 92, 88,113, 85,  //40
             79,118,105, 83, 67,114,119, 95, 99,109,188,253,253,253,253,253,  //50
            253, 72, 70, 80, 81, 60, 96, 93, 89, 68,120, 97, 77, 86, 69, 55,  //60
             78,115, 65, 66, 58, 76,106,103, 87,107,112,253,253,253,253,253,  //70
            255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,  //80
            255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,  //90
            +253,233, 61,253,253,253,253,253,253,253,253,253,253, 74,253,253,  //a0
            253,253,253,253,247,253,253, 36, 46, 71, 73,253, 54,253,108,123,  //b0
            110, 31, 51, 43, 41, 34, 91, 40, 52, 47, 44, 53, 38, 49, 59, 39,  //c0
             35, 48,250, 37, 33, 45, 56, 50, 84, 57,120,121, 17, 18, 22, 15,  //d0
            124,  1, 29, 20, 21,  3, 32, 13, 25,  5, 11, 16, 10,  6, 30,  4,  //e0
              9,  8, 14,  7,  2, 12, 28, 23, 42, 24, 64, 75, 19, 26, 27,253,  //f0
        };    
        
        public Win1253Model() : base(WIN1253__CHAR_TO_ORDER_MAP, Charsets.WIN1253)
        {
        }
    }
}
