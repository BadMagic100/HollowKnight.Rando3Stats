using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    public class SpriteLoader
    {
        private readonly string resourceNamespace;
        private readonly Assembly asm;
        private readonly Dictionary<string, Texture2D> textures = new();

        public SpriteLoader(Assembly asm, string resourceNamespace)
        {
            this.asm = asm;
            this.resourceNamespace = resourceNamespace;
        }

        private Texture2D LoadEmbeddedTexture(string resourceName)
        {
            using Stream imageStream = asm.GetManifestResourceStream(resourceName);
            byte[] buffer = new byte[imageStream.Length];
            imageStream.Read(buffer, 0, buffer.Length);

            Texture2D tex = new(1, 1);
            tex.LoadImage(buffer.ToArray());

            return tex;
        }

        public void Preload()
        {
            if (textures.Count != 0)
            {
                throw new InvalidOperationException("You can only preload images once, and only before manually loading any textures.");
            }
            foreach (string longName in asm.GetManifestResourceNames())
            {
                if (!longName.StartsWith($"{resourceNamespace}.")) continue;
                IEnumerable<string> fragments = longName.Split('.');
                string shortName = string.Join(".", fragments.Skip(fragments.Count() - 2).ToArray());
                textures[shortName] = LoadEmbeddedTexture(longName);
            }
        }

        public Texture2D GetTexture(string name)
        {
            if (!textures.ContainsKey(name))
            {
                string longName = $"{resourceNamespace}.{name}";
                if (asm.GetManifestResourceNames().Contains(longName))
                {
                    textures[name] = LoadEmbeddedTexture(longName);
                }
                else
                {
                    throw new FileNotFoundException("No resource with the given name. Make sure the file exists and is compiled as an embedded resource.");
                }
            }
            return textures[name];
        }

        public void DisposeTexture(string name)
        {
            textures.Remove(name);
        }
    }
}
