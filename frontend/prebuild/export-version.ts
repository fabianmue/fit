#!/usr/bin/env node

import fs from 'fs';
import { execSync } from 'child_process';

import packageJson from '../package.json' with { type: 'json' };

const commitHash = execSync('git rev-parse --short HEAD').toString().trim();
const content = `{ 
  "version": "${packageJson.version}",
  "commitHash": "${commitHash}",
  "buildDate": "${new Date().toISOString()}"
}\n`;

fs.writeFileSync('src/version.json', content.toString());
