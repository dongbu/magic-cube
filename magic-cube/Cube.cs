﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace magic_cube {
    /// <summary>
    /// Designates a standard naming of a cube's sides or faces
    /// </summary>
    public enum CubeFace {
        F, // front
        R, // right
        B, // back
        L, // left
        U, // up
        D, // down
    }

    /// <summary>
    /// Create a cube
    /// 
    /// In order to display the cube to a <see cref="ViewPort3D"/>:
    /// <code>
    /// Cube c = new Cube(new Point3D(0, 0, 0), 1, new Dictionary&lt;CubeFace, Material&gt;() {
    ///     {CubeFace.F, new DiffuseMaterial(new SolidColorBrush(Colors.White))},
    ///     {CubeFace.R, new DiffuseMaterial(new SolidColorBrush(Colors.Blue))},
    ///     {CubeFace.U, new DiffuseMaterial(new SolidColorBrush(Colors.Yellow))},
    /// });
    /// 
    /// ModelVisual3D m = new ModelVisual3D();
    /// m.Content = c.group;
    /// 
    /// this.mainViewport.Children.Add(m);
    /// </code>
    /// </summary>
    public class Cube {
        private Point3D origin;
        private int size;

        public Model3DGroup group { get; private set;}

        private Material defaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Black));
        private Dictionary<CubeFace, Material> faces;

        /// <summary>
        /// Cube constructor
        /// </summary>
        /// <param name="o">The origin of the cube, this will always be the far-lower-left corner</param>
        /// <param name="size">The length of the edge</param>
        /// <param name="f">This parameter allows the use of different materials on the cube's faces</param>
        /// <param name="defaultMaterial">The material to be applied on the faces 
        /// that are not included in the previous parameter.
        /// This defaults to a solid black diffuse material
        /// </param>
        public Cube(Point3D o, int size, Dictionary<CubeFace, Material> f, Material defaultMaterial=null) {
            this.group = new Model3DGroup();
            this.origin = o;
            this.size = size;
            this.faces = f;

            if(defaultMaterial != null){
                this.defaultMaterial = defaultMaterial;
            }

            createCube();
        }
        
        /// <summary>
        /// Creates the cube by creating it's 6 faces
        /// If the class was instantiated with a valid <see cref="f"/> dictionary 
        /// then each face will get the corespondent Material, 
        /// otherwise <see cref="defaultMaterial"/> will be used
        /// </summary>
        private void createCube(){
            Material material;

            foreach (var face in Enum.GetValues(typeof(CubeFace)).Cast<CubeFace>()){
                if (faces == null || !faces.TryGetValue(face, out material)) {
                    material = defaultMaterial;
                }

                createFace(face, material);		 
	        }
        }

        /// <summary>
        /// Create a face of the cube
        /// </summary>
        /// <param name="f">The face that needs to be created</param>
        /// <param name="m">Materal to be applied to the face</param>
        private void createFace(CubeFace f, Material m) {
            Point3D p0 = new Point3D();
            Point3D p1 = new Point3D();
            Point3D p2 = new Point3D();
            Point3D p3 = new Point3D();
            
            switch (f) {
                case CubeFace.F:
                    /**
                     *  /--------/
                     * 0-------3 |
                     * |       | |
                     * |       | /
                     * 1-------2/
                     */
                    p0.X = origin.X;
                    p0.Y = origin.Y + size;
                    p0.Z = origin.Z + size;

                    p1.X = origin.X;
                    p1.Y = origin.Y;
                    p1.Z = origin.Z + size;

                    p2.X = origin.X + size;
                    p2.Y = origin.Y;
                    p2.Z = origin.Z + size;

                    p3.X = origin.X + size;
                    p3.Y = origin.Y + size;
                    p3.Z = origin.Z + size;
                    break;
                case CubeFace.R:
                    /**
                     *  /--------3
                     * /-------0 |
                     * |       | |
                     * |       | 2
                     * |-------1/
                     */
                    p0.X = origin.X + size;
                    p0.Y = origin.Y + size;
                    p0.Z = origin.Z + size;

                    p1.X = origin.X + size;
                    p1.Y = origin.Y;
                    p1.Z = origin.Z + size;
                                        
                    p2.X = origin.X + size;
                    p2.Y = origin.Y;
                    p2.Z = origin.Z;
                    
                    p3.X = origin.X + size;
                    p3.Y = origin.Y + size;
                    p3.Z = origin.Z;
                    break;
                case CubeFace.B:
                    /**
                     *  3--------0
                     * /-------/ |
                     * | |     | |
                     * | 2 ----|-1
                     * |-------|/
                     */                    
                    p0.X = origin.X + size;
                    p0.Y = origin.Y + size;
                    p0.Z = origin.Z; 

                    p1.X = origin.X + size;
                    p1.Y = origin.Y;
                    p1.Z = origin.Z;

                    p2 = origin;

                    p3.X = origin.X;
                    p3.Y = origin.Y + size;
                    p3.Z = origin.Z;
                    break;
                case CubeFace.L:
                    /**
                     *  0--------/
                     * 3-------/ |
                     * | |     | |
                     * | 1 ----|-/
                     * 2-------|/
                     */                    
                    p0.X = origin.X;
                    p0.Y = origin.Y + size;
                    p0.Z = origin.Z;

                    p1 = origin;

                    p2.X = origin.X;
                    p2.Y = origin.Y;
                    p2.Z = origin.Z + size;

                    p3.X = origin.X;
                    p3.Y = origin.Y + size;
                    p3.Z = origin.Z + size;
                    break;
                case CubeFace.U:
                    /**
                     *  0--------3
                     * 1-------2 |
                     * |       | |
                     * |       | |
                     * |-------|/
                     */       
                    p0.X = origin.X;
                    p0.Y = origin.Y + size;
                    p0.Z = origin.Z;  
                 
                    p1.X = origin.X;
                    p1.Y = origin.Y + size;
                    p1.Z = origin.Z + size;

                    p2.X = origin.X + size;
                    p2.Y = origin.Y + size;
                    p2.Z = origin.Z + size;
                    
                    p3.X = origin.X + size;
                    p3.Y = origin.Y + size;
                    p3.Z = origin.Z;
                    break;
                case CubeFace.D:
                    /**
                     *  /--------/
                     * /-------/ |
                     * | |     | |
                     * | 0 ----|-1
                     * 3-------|2
                     */
                    p0 = origin;
                    
                    p1.X = origin.X + size;
                    p1.Y = origin.Y;
                    p1.Z = origin.Z;
                    
                    p2.X = origin.X + size;
                    p2.Y = origin.Y;
                    p2.Z = origin.Z + size;

                    p3.X = origin.X;
                    p3.Y = origin.Y;
                    p3.Z = origin.Z + size;
                    break;
            }

            group.Children.Add(Helpers.createTriangleModel(p0, p1, p2, m));
            group.Children.Add(Helpers.createTriangleModel(p0, p2, p3, m));
        }
    }
}
