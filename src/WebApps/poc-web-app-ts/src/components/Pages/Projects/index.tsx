import React, { useContext, useState, useEffect } from 'react';
import { BffApiContext } from '../../../service/BffApiProvider';
import { GroupOfIssues } from '../../../service/types';
import ProjectsList from './ProjectsList';
export interface ProjectsProperties {}

const Projects: React.FC<ProjectsProperties> = () => {
  const [projects, setProjects] = useState<GroupOfIssues[]>([]);

  const [isLoading, setIsLoading] = useState(true);
  const bffApi = useContext(BffApiContext);

  const getAllItems = () => {
    setIsLoading(true);
    bffApi
      ?.GetAllGroupsOfIssues()
      .then((response) => {
        setProjects(response ?? []);
      })
      .catch(console.error)
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    getAllItems();
  }, []);

  return (
    <div>
      <h1>
        {projects === null ? 'loading' : <ProjectsList projects={projects} />}
      </h1>
    </div>
  );
};

export default Projects;
