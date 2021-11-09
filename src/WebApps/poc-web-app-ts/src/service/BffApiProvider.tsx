import React from 'react';
import { BffApi } from './BffApi';
export const BffApiContext = React.createContext<BffApi | undefined>(undefined);

interface BffApiProviderProps {
  baseUri: string;
}

class BffApiProvider extends React.Component<BffApiProviderProps> {
  bffApi: BffApi;

  constructor(props: BffApiProviderProps) {
    super(props);
    const { baseUri } = this.props;
    this.bffApi = new BffApi(baseUri);
  }

  render() {
    const { children } = this.props;

    return (
      <BffApiContext.Provider value={this.bffApi}>
        {children}
      </BffApiContext.Provider>
    );
  }
}

export default BffApiProvider;
