import { SYSTEM_ERROR } from '../config/CONSTANTS';
import axios from 'axios';
import { GET_ALL_GROUPSOFISSUES } from './CONSTANTS';

export const getAllGroupsOfIssues = () => {
  console.log('groupOfIssueServices > getAllGroupsOfIssues called...');
  return new Promise((resolve, reject) => {
    try {
      axios
        .get(GET_ALL_GROUPSOFISSUES())
        .then((res) => {
          console.log('getAllGroupsOfIssues > axios res=', res);
          resolve(res.data);
        })
        .catch((err) => {
          console.log('getAllGroupsOfIssues > axios err=', err);
          reject('Error in getAllGroupsOfIssues axios!');
        });
    } catch (error) {
      console.error(
        'in groupOfIssueServices > getAllGroupsOfIssues, Err===',
        error
      );
      reject(SYSTEM_ERROR);
    }
  });
};
