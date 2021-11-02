import React, { useEffect, useState } from 'react';
import { ProjectsView } from './ProjectsView';
import { Link } from 'react-router-dom';
import { ROOT } from '../../navigation/CONSTANTS';
import { CircularProgress, Typography } from '@material-ui/core';
import { getAllGroupsOfIssues } from '../../services';
import { ProjectsList } from './ProjectList';

export const ProjectsContainer = () => {
  const [groupsOfIssues, setGroupsOfIssues] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setIsLoading(true);
    getAllGroupsOfIssues()
      .then((res) => {
        console.log('Dashboard > getAllGroupsOfIssues > res=', res);
        setGroupsOfIssues(res);
        setIsLoading(false);
      })
      .catch((err) => {
        console.log('axios err=', err);
        setGroupsOfIssues([]);
        setIsLoading(false);
      });

    return () => {
      console.log('axios cleanup.');
    };
  }, []);

  const NoGroupsOfIssuesList = (
    <Typography variant='body2'>No projects found!</Typography>
  );

  return (
    <div>
      <Link to={ROOT}>Home</Link>
      <ProjectsView />
      {isLoading ? (
        <CircularProgress />
      ) : groupsOfIssues ? (
        <ProjectsList projects={groupsOfIssues} />
      ) : (
        NoGroupsOfIssuesList
      )}
    </div>
  );
};
