import React from 'react';
import {
  CircularProgress,
  Typography,
  Grid,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
} from '@mui/material';
import { GroupOfIssues } from '../../../service/types';

export interface ProjectListProperites {
  projects: GroupOfIssues[];
}
export const ProjectsList: React.FC<ProjectListProperites> = (props) => {
  return (
    <Grid container spacing={2}>
      <Grid item xs={12} md={6}>
        {/* <Typography variant="h6" className={classes.title}> */}
        <Typography variant='h6'>
          List of projects fetched from jsonplaceholder:
        </Typography>
        <div>
          {console.log('gurwa')}
          {console.log(props.projects)}
          {props.projects.map((project, index) => {
            console.log(project);
          })}

          <List dense={false}>
            {props.projects.map((project, index) => (
              <ListItem key={project.id}>
                <ListItemText primary={project.name} />
                <ListItemText primary={project.shortName} />
                <ListItemText primary={project.typeOfGroupId} />
              </ListItem>
            ))}
          </List>
        </div>
      </Grid>
    </Grid>
  );
};

export default ProjectsList;
