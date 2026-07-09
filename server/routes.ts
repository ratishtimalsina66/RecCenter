import type { Express } from "express";
import type { Server } from "http";
import { storage } from "./storage";
import { api } from "@shared/routes";
import { z } from "zod";

export async function registerRoutes(
  httpServer: Server,
  app: Express
): Promise<Server> {
  app.get(api.profile.get.path, async (req, res) => {
    const profile = await storage.getProfile();
    res.json(profile);
  });

  app.get(api.projects.list.path, async (req, res) => {
    const projects = await storage.getProjects();
    res.json(projects);
  });

  app.get(api.skills.list.path, async (req, res) => {
    const skills = await storage.getSkills();
    res.json(skills);
  });

  app.get(api.experience.list.path, async (req, res) => {
    const exp = await storage.getExperience();
    res.json(exp);
  });

  app.get(api.education.list.path, async (req, res) => {
    const edu = await storage.getEducation();
    res.json(edu);
  });

  app.post(api.contact.submit.path, async (req, res) => {
    try {
      const input = api.contact.submit.input.parse(req.body);
      const message = await storage.createMessage(input);
      res.status(201).json(message);
    } catch (err) {
      if (err instanceof z.ZodError) {
        return res.status(400).json({
          message: err.errors[0].message,
          field: err.errors[0].path.join('.'),
        });
      }
      throw err;
    }
  });

  // Re-seed database with correct information from the resume image
  // Checking if profile exists, but we'll force update it this time
  await seedDatabase();

  return httpServer;
}

async function seedDatabase() {
  const currentProfile = await storage.getProfile();
  if (!currentProfile) {
    await storage.createProfile({
      name: "Ratish Timalsina",
      title: "Information Technology Student & Full-Stack Developer",
      summary: "Information Technology student (B.S., May 2027) with hands-on experience in networking, system administration, web development, database design, and technical support. Skilled in building full-stack applications, troubleshooting technology issues, and supporting reliable IT solutions. Familiar with AI-assisted development, prompt engineering, generative AI tools, and configuring simple AI workflows. Seeking an internship opportunity to apply technical skills while continuing to grow in software, infrastructure, and emerging technologies.",
      email: "rtimals@rockets.utoledo.edu",
      phone: "573-355-2475",
      location: "Toledo, OH",
      github: "",
      linkedin: "linkedin.com/in/ratish-timalsina",
    });

    await storage.createExperience({
      company: "The University of Toledo / Chartwells",
      position: "Student Worker",
      location: "Toledo, OH",
      duration: "Jan 2023 - Dec 2023",
      description: "Delivered high-quality customer service in a fast-paced environment. Demonstrated reliability, teamwork, and strong time-management skills. Resolved operational challenges and supported daily business operations. Collaborated with team members to maintain efficient workflows and customer satisfaction.",
    });

    await storage.createEducation({
      institution: "The University of Toledo",
      degree: "Bachelor of Science in Information Technology",
      duration: "Expected May 2027",
    });

    const skillSet = [
      { name: "JavaScript", category: "Programming & Scripting", proficiency: 85 },
      { name: "Python", category: "Programming & Scripting", proficiency: 75 },
      { name: "PHP", category: "Programming & Scripting", proficiency: 75 },
      { name: "PowerShell", category: "Programming & Scripting", proficiency: 65 },
      { name: "HTML/CSS", category: "Programming & Scripting", proficiency: 90 },
      { name: "SQL", category: "Programming & Scripting", proficiency: 80 },
      { name: "Node.js (Express)", category: "Web & Frameworks", proficiency: 85 },
      { name: "RESTful APIs", category: "Web & Frameworks", proficiency: 80 },
      { name: "JSON", category: "Web & Frameworks", proficiency: 85 },
      { name: "Responsive Web Design", category: "Web & Frameworks", proficiency: 85 },
      { name: "MySQL", category: "Databases", proficiency: 80 },
      { name: "Relational Database Design", category: "Databases", proficiency: 80 },
      { name: "TCP/IP / LAN / WAN", category: "Systems & Networking", proficiency: 75 },
      { name: "VPN / Switching & Routing", category: "Systems & Networking", proficiency: 70 },
      { name: "Cisco Packet Tracer", category: "Systems & Networking", proficiency: 70 },
      { name: "Office 365 / VMware", category: "Systems & Networking", proficiency: 75 },
      { name: "Git / GitHub", category: "Tools", proficiency: 85 },
      { name: "VS Code", category: "Tools", proficiency: 90 },
      { name: "Postman", category: "Tools", proficiency: 75 },
      { name: "Prompt Engineering / AI Workflows", category: "AI & Emerging Tech", proficiency: 75 },
    ];

    for (const skill of skillSet) {
      await storage.createSkill(skill);
    }

    await storage.createProject({
      title: "Timalsina Masala Pasal — E-Commerce Web App",
      description: "A full-stack e-commerce platform for a family spice brand built with HTML, CSS, JavaScript, and Node.js (Express). Features REST APIs, order tracking, an admin dashboard, and sales analytics.",
      imageUrl: "/projects/timalsina-masala.png",
      projectUrl: "",
      repoUrl: "",
      tags: ["Node.js", "Express", "JavaScript", "REST API"],
    });

    await storage.createProject({
      title: "MedRx Pharmacy Database System",
      description: "A pharmacy management system built with PHP and MySQL, featuring normalized database structures and complex SQL queries for prescription management.",
      imageUrl: "/projects/medrx-pharmacy-prescriptions.png",
      projectUrl: "",
      repoUrl: "",
      tags: ["PHP", "MySQL", "SQL"],
    });

    await storage.createProject({
      title: "Field Journal — Travel Website",
      description: "A responsive multi-page website with interactive galleries, charts, accordions, and dynamic UI features, built with HTML, CSS, and JavaScript.",
      imageUrl: "/projects/field-journal.png",
      projectUrl: "",
      repoUrl: "",
      tags: ["HTML", "CSS", "JavaScript"],
    });

    await storage.createProject({
      title: "The Garage — Used Car Dealership Website",
      description: "A professional multi-page dealership website showcasing inventory, services, and business information, built with responsive design principles.",
      imageUrl: "/projects/the-garage.png",
      projectUrl: "",
      repoUrl: "",
      tags: ["HTML", "CSS", "JavaScript"],
    });

    await storage.createProject({
      title: "AI Knowledge Assistant",
      description: "A simple AI-powered assistant built using prompt engineering techniques and structured knowledge resources, with reusable prompts and AI workflow automation concepts.",
      imageUrl: "",
      projectUrl: "",
      repoUrl: "",
      tags: ["Prompt Engineering", "Generative AI", "AI Workflow Design"],
    });
  }
}
